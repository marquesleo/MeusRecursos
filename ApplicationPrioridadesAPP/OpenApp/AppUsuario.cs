using ApplicationPrioridadesAPP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Prioridades.Interface;
using Domain.Prioridades.ViewModels;
using Domain.Prioridades.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Prioridades.Services;
using System.Linq;
using Domain.Prioridades.Interfaces;
using Microsoft.VisualBasic;
using ApplicationPrioridadesAPP.Helpers;
using Microsoft.Extensions.Options;
using ApplicationPrioridadesAPP.Authorization;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _IUsuario;
        private readonly IRefreshToken _IRefreshToken;
        private readonly AppSettings _appSettings;
        private IJwtUtils _jwtUtils;


        public AppUsuario(IUsuario IUsuario,
                          IRefreshToken IRefreshToken,
                          IOptions<AppSettings> appSettings,
                          IJwtUtils jwtUtils)
        {
            this._IUsuario = IUsuario;
            this._IRefreshToken = IRefreshToken;
            this._appSettings = appSettings.Value;
            this._jwtUtils = jwtUtils;
        }

        public async Task AddUsuario(LoginViewModel loginViewModel)
        {

            var usuario = new Usuario();
            usuario.Username = loginViewModel.Username.ToUpper();
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
            usuario.Email = loginViewModel.Email;
                               
            await _IUsuario.Add(usuario);
            loginViewModel.Id = usuario.Id;
        }

        public async Task Delete(Usuario objeto)
        {
           await _IUsuario.Delete(objeto);
        }

        public async Task<List<Usuario>> FindByCondition(Expression<Func<Usuario, bool>> expression)
        {
            return await _IUsuario.FindByCondition(expression);
        }

        public async Task<Usuario> GetEntityById(Guid id)
        {
            var usuario = await _IUsuario.GetEntityById(id);
            if (usuario != null) { 
                 usuario.RefreshTokens = await _IRefreshToken.FindByCondition(r => r.UsuarioId == usuario.Id);
            }
            return Criptografar(usuario);
        }

        private Usuario Criptografar(Usuario usuario){
            if (usuario != null)
                usuario.Password = Utils.Criptografia.Decriptografar(usuario.Password);
            else
                usuario = new Usuario();

           return usuario;

        }


        public bool IsUsuarioExiste(string login)
        {
            var usuario = this.ObterUsuario(login).Result;
            if (usuario == null || usuario.Id == Guid.Empty)
               return false;
            else
               return true;
        }

        public async Task<List<Usuario>> List()
        {
            return await _IUsuario.List();
        }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            var usuario = await _IUsuario.ObterUsuario(login, senha);
            return  Criptografar(usuario);;
        }

        public async Task<Usuario> ObterUsuario(string login)
        {
            var usuario = await _IUsuario.ObterUsuario(login);
            return Criptografar(usuario);;
        }

        public async Task UpdateUsuario(LoginViewModel loginViewModel)
        {
             var usuario = new Domain.Prioridades.Entities.Usuario();
            usuario.Username = loginViewModel.Username.ToUpper();
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
            usuario.Email = loginViewModel.Email;
            usuario.Id = loginViewModel.Id;
            await _IUsuario.Update(usuario);
        }

        private string generateJwtToken(Usuario user)
        {
           return TokenService.GenerateToken(user);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {

            var refresh = await _IRefreshToken.FindByCondition(r => r.Token == token);
            var usuario = await _IUsuario.GetEntityById(refresh.SingleOrDefault().UsuarioId);
            if (refresh != null && refresh.SingleOrDefault().IsRevoked)
            {
                
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refresh.SingleOrDefault(), usuario, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
               
                await _IRefreshToken.Update(refresh.SingleOrDefault());
              
            }

            if (!refresh.SingleOrDefault().IsActive)
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refresh.SingleOrDefault(), ipAddress);
            usuario.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(usuario);

            // save changes to db
            await _IUsuario.Update(usuario);

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(usuario);

            return new AuthenticateResponse(usuario, jwtToken, newRefreshToken.Token);

        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _IUsuario.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = _IRefreshToken.FindByCondition(x => x.Token == token).Result.Single();

            // return false if token is not active
            if (!refreshToken.IsActive) throw new AppException("Invalid token");

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            await _IUsuario.Update(user.SingleOrDefault());

            return true;
        }


        public AuthenticateResponse Authenticate(Usuario usuario, string ipAddress)
        {
            try
            {
                var jwtToken = generateJwtToken(usuario);
                var refreshToken = generateRefreshToken(ipAddress);
              //  refreshToken.Token = jwtToken;
                // save refresh token
              
                refreshToken.Id = Guid.NewGuid();
                refreshToken.UsuarioId = usuario.Id;
                usuario.RefreshTokens.Add(refreshToken);

                removeOldRefreshTokens(usuario);


                _IRefreshToken.Add(refreshToken);
                return new AuthenticateResponse(usuario, jwtToken, refreshToken.Token);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            // authentication successful so generate jwt and refresh tokens
      
           
        }

        private void removeOldRefreshTokens(Usuario user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private async Task<Usuario> getUserByRefreshToken(string token)
        {
            var user = await _IUsuario.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user.SingleOrDefault() == null)
                throw new AppException("Invalid token");

            return user.SingleOrDefault();
        }

        
        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Usuario user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
