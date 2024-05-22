using ApplicationPrioridadesAPP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Prioridades.Interface;
using Domain.Prioridades.ViewModels;
using Domain.Prioridades.Entities;
using System.Security.Cryptography;

using Domain.Prioridades.Services;
using System.Linq;
using Domain.Prioridades.Interfaces;
using ApplicationPrioridadesAPP.Helpers;
using Microsoft.Extensions.Options;
using ApplicationPrioridadesAPP.Authorization;
using static Domain.Prioridades.Services.TokenService;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario
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
            _IUsuario = IUsuario;
            _IRefreshToken = IRefreshToken;
            _appSettings = appSettings.Value;
            _jwtUtils = jwtUtils;
        }


        [Obsolete]
        public async Task AddUsuario(LoginViewModel loginViewModel)
        {

            var usuario = new Domain.Prioridades.Entities.Usuario();
            usuario.Username = loginViewModel.Username.ToUpper();
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
            usuario.Email = loginViewModel.Email;

            await _IUsuario.Add(usuario);
            loginViewModel.Id = usuario.Id;
        }

        public async Task Delete(Domain.Prioridades.Entities.Usuario objeto)
        {
            await _IUsuario.Delete(objeto);
        }

        public async Task<List<Domain.Prioridades.Entities.Usuario>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.Usuario, bool>> expression)
        {
            return await _IUsuario.FindByCondition(expression);
        }

        [Obsolete]
        public async Task<Domain.Prioridades.Entities.Usuario> GetEntityById(Guid id)
        {
            var usuario = await _IUsuario.GetEntityById(id);
            if (usuario != null)
            {
                usuario.RefreshTokens = await _IRefreshToken.FindByCondition(r => r.UsuarioId == usuario.Id);
            }
            return Criptografar(usuario);
        }

        private Domain.Prioridades.Entities.Usuario Criptografar(Domain.Prioridades.Entities.Usuario usuario)
        {
            if (usuario != null)
                usuario.Password = Utils.Criptografia.Decriptografar(usuario.Password);
            else
                usuario = new Domain.Prioridades.Entities.Usuario();

            return usuario;

        }


        public bool IsUsuarioExiste(string login)
        {
            var usuario = ObterUsuario(login).Result;
            if (usuario == null || usuario.Id == Guid.Empty)
                return false;
            else
                return true;
        }


        public async Task<bool> IsUsuarioExiste(Domain.Prioridades.Entities.Usuario usuario)
        {
            var dbUsuario = await ObterUsuario(usuario.Id);
            if (dbUsuario != null && dbUsuario.Username.ToLower().Equals(usuario.Username.ToLower()) && !dbUsuario.Id.Equals(usuario.Id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsUsuarioComEmailExistente(Domain.Prioridades.Entities.Usuario usuario)
        {
            var dbUsuario = await FindByCondition(p => p.Email == usuario.Email);

            if (dbUsuario != null && dbUsuario.Any())
            {
                var usuarioUnico = dbUsuario.FirstOrDefault();

                if (usuarioUnico != null && usuarioUnico.Email.ToLower().Equals(usuario.Email.ToLower()) && usuarioUnico.Id != usuario.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUsuarioComEmailExistente(string email)
        {
            var usuario = FindByCondition(p => p.Email == email).Result;
            if (usuario == null || !usuario.Any())
                return false;
            else
                return true;
        }

        public async Task<List<Domain.Prioridades.Entities.Usuario>> List()
        {
            return await _IUsuario.List();
        }

        public async Task<Domain.Prioridades.Entities.Usuario> ObterUsuario(string login, string senha)
        {
            var usuario = await _IUsuario.ObterUsuario(login, senha);
            return Criptografar(usuario); ;
        }



        public async Task<Domain.Prioridades.Entities.Usuario> ObterUsuario(string login)
        {
            var usuario = await _IUsuario.ObterUsuario(login);
            return Criptografar(usuario); ;
        }


        [Obsolete]
        public async Task UpdateUsuario(LoginViewModel loginViewModel)
        {
            var usuario = new Domain.Prioridades.Entities.Usuario();
            usuario.Username = loginViewModel.Username.ToUpper();
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
            usuario.Email = loginViewModel.Email;
            usuario.Id = loginViewModel.Id;
            await _IUsuario.Update(usuario);
        }

        private MeuToken generateJwtToken(Domain.Prioridades.Entities.Usuario user)
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

        public async Task<AuthenticateResponse> RefreshToken(string token,
                                                             string refreshToken,
                                                             string ipAddress)
        {
            try
            {


                Domain.Prioridades.Entities.Usuario usuario = null;
                var refresh = await _IRefreshToken.FindByCondition(r => r.Token == refreshToken);
                usuario = await _IUsuario.GetEntityById(refresh.SingleOrDefault().UsuarioId);

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

                //remove old refresh tokens from user
                removeOldRefreshTokens(usuario);

                //save changes to db
                await _IUsuario.Update(usuario);
                await _IRefreshToken.Update(refresh.SingleOrDefault());
                // generate new jwt
                var jwtToken = TokenService.GenerateToken(usuario);
                return new AuthenticateResponse(usuario, jwtToken.token, newRefreshToken.Token, jwtToken.expira);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        public AuthenticateResponse Authenticate(Domain.Prioridades.Entities.Usuario usuario, string ipAddress)
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
                return new AuthenticateResponse(usuario, jwtToken.token, refreshToken.Token, jwtToken.expira);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            // authentication successful so generate jwt and refresh tokens


        }

        private void removeOldRefreshTokens(Domain.Prioridades.Entities.Usuario user)
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

        private async Task<Domain.Prioridades.Entities.Usuario> getUserByRefreshToken(string token)
        {
            var user = await _IUsuario.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user.SingleOrDefault() == null)
                throw new AppException("Invalid token");

            return user.SingleOrDefault();
        }


        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Domain.Prioridades.Entities.Usuario user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                /*  var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                  if (childToken.IsActive)
                      revokeRefreshToken(childToken, ipAddress, reason);
                  else
                      revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);*/
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReplacedByToken = replacedByToken;
        }

        public async Task AddUsuario(Domain.Prioridades.Entities.Usuario usuario)
        {
            if (this.IsUsuarioExiste(usuario.Username))
                throw new Exceptions.UsuarioDuplicadoException();

            if (this.IsUsuarioComEmailExistente(usuario.Email))
                throw new Exceptions.UsuarioComEmailExistenteException();

            await _IUsuario.Add(usuario);

        }

        public async Task UpdateUsuario(Domain.Prioridades.Entities.Usuario usuario)
        {
           if (await this.IsUsuarioExiste(usuario))
                throw new Exceptions.UsuarioDuplicadoException();


            if (await this.IsUsuarioComEmailExistente(usuario))
                throw new Exceptions.UsuarioComEmailExistenteException();

            await _IUsuario.Update(usuario);
        }

        public async Task<Domain.Prioridades.Entities.Usuario> ObterUsuario(Guid id)
        {
            var usuario = await _IUsuario.GetEntityById(id);
            return usuario;
        }
    }
}
