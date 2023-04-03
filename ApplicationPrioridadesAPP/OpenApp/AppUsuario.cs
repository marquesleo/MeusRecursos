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

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _IUsuario;
        private readonly IRefreshToken _IRefreshToken;
        public AppUsuario(IUsuario IUsuario,
                          IRefreshToken IRefreshToken)
        {
            this._IUsuario = IUsuario;
            this._IRefreshToken = IRefreshToken;
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

            if (refresh != null && refresh.Any())
            {
                var user = await _IUsuario.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token));

                // return null if no user found with token
                if (user == null) return null;

                var refreshToken = await _IRefreshToken.FindByCondition(x => x.Token == token);

                if (refreshToken != null && refreshToken.Any())
                {
                    // return null if token is no longer active
                    if (!refreshToken.SingleOrDefault().IsActive) return null;

                    // replace old refresh token with a new one and save
                    var newRefreshToken = generateRefreshToken(ipAddress);
                    refreshToken.SingleOrDefault().Revoked = DateTime.UtcNow;
                    refreshToken.SingleOrDefault().RevokedByIp = ipAddress;
                    refreshToken.SingleOrDefault().ReplacedByToken = newRefreshToken.Token;
                    refreshToken.SingleOrDefault().UsuarioId = user.FirstOrDefault().Id;

                    user.FirstOrDefault().RefreshTokens.Add(newRefreshToken);
                    await _IRefreshToken.Update(refreshToken.SingleOrDefault());
                    // generate new jwt
                    var jwtToken = generateJwtToken(user.FirstOrDefault());
                    return new AuthenticateResponse(user.SingleOrDefault(), jwtToken, newRefreshToken.Token);
                }
                return new AuthenticateResponse(user.FirstOrDefault(), "", "");
            }

            return null;
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _IUsuario.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = _IRefreshToken.FindByCondition(x => x.Token == token).Result.Single();

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
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
                _IRefreshToken.Add(refreshToken);
                return new AuthenticateResponse(usuario, jwtToken, refreshToken.Token);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            // authentication successful so generate jwt and refresh tokens
      
           
        }

    }
}
