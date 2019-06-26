using Dapper;
using ProMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProMarket.Data.Repositorio
{
    public class ClienteRepositorio
    {
        public List<ClienteCompletoEntidade> RetornarClientes(string ConnectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    return null;

                var retorno = new System.Data.SqlClient.SqlConnection(ConnectionString).Query<ClienteCompletoEntidade>(RetornaQueryListaCliente()).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }         
        }
        public string RetornaQueryListaCliente()
        {
            return $@"SELECT CL.NOME,
                        CA.DESCRICAO AS DESCRICAO_CATEGORIA
                        FROM CLIENTE CL 
                        JOIN CATEGORIA CA ON CA.ID_CATEGORIA = CL.ID_CATEGORIA";
        }
    }
}
