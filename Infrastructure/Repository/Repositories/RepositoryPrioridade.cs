using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Infrastructure.Configuration;
using Npgsql;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryPrioridade : Generics.RepositoryGeneric<Prioridade>, IPrioridade
    {
        public RepositoryPrioridade(MyDB myDB):base(myDB){ }

        public async Task Down(Prioridade prioridade)
        {
              try{
        
                    var con = new Npgsql.NpgsqlConnection(base.myDB.getStringConn().conexao);
                    var sql = " CALL personal.sp_down(@id); ";
                    con.Open();
                    var cmd = new Npgsql.NpgsqlCommand(sql,con);
                    cmd.Parameters.Add(new NpgsqlParameter("@id", prioridade.Id));
                    cmd.ExecuteNonQuery();
                    return; 
            }catch(Exception ex) {
                throw ex;   
            } 
        }

        public async Task Up(Prioridade prioridade)
        {
            try{
        
                    var con = new Npgsql.NpgsqlConnection(base.myDB.getStringConn().conexao);
                    var sql = " CALL personal.sp_up(@id); ";
                    con.Open();
                    var cmd = new Npgsql.NpgsqlCommand(sql,con);
                    cmd.Parameters.Add(new NpgsqlParameter("@id", prioridade.Id));
                    cmd.ExecuteNonQuery();
                    return; 
            }catch(Exception ex) {
                throw ex;   
            } 
        }

        public async Task InserirPrioridade(Prioridade prioridade)
        {
            try{
        
                    var con = new Npgsql.NpgsqlConnection(base.myDB.getStringConn().conexao);
                    var sql = " CALL personal.sp_insertprioridade(@vdescricao,@vativo,@vfeito,@vusuario_id); ";
                    con.Open();
                    var cmd = new Npgsql.NpgsqlCommand(sql,con);
                    cmd.Parameters.AddRange(RetornarParametro(prioridade).ToArray());
                    cmd.ExecuteNonQuery();
                    return; 
            }catch(Exception ex) {
                throw ex;
            } 
           
        }

      

        protected  List<NpgsqlParameter> RetornarParametro(Prioridade objeto) {
            var lstParametro = new List<NpgsqlParameter>();
           
           
            lstParametro.Add(new NpgsqlParameter("@vdescricao",objeto.Descricao));
            lstParametro.Add(new NpgsqlParameter("@vativo",objeto.Ativo));
            lstParametro.Add(new NpgsqlParameter("@vfeito",objeto.Feito));
            lstParametro.Add(new NpgsqlParameter("@vusuario_id",objeto.Usuario.Id));
            return lstParametro; 
        }

    }
}
