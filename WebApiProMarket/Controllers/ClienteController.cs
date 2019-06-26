using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProMarket.Data.Entity;
using SampleWebApiAspNetCore.Configuration;
using System.Collections.Generic;
using WebApiProMarket.Models;

namespace SampleWebApiAspNetCore.v2.Controllers
{


    public class ClienteController : ControllerBase
    {
        private IOptions<AppSettings> _appSettings { get; set; }
        public ClienteController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet("BuscarClientes")]
        public JsonResult BuscarClientes()
        {
            //alterar o banco
            var connectionString = "Initial Catalog=BASE_DESENV;Data Source=DESKTOP-4K83T95;Integrated Security=SSPI;";

            var retornarCliente = new ProMarket.Data.Repositorio.ClienteRepositorio().RetornarClientes(_appSettings.Value.ConnectString ?? connectionString);
            if(retornarCliente == null && retornarCliente.Count < 0)
                return new JsonResult(new { Mensagem = "ERRO: Ocorreu um problema durante o processo." });

            return new JsonResult(new { Cliente = ConverterParaObjetoDeRetorno(retornarCliente) });
        }

        #region metodos privados
        private List<ClienteCompleto> ConverterParaObjetoDeRetorno(List<ClienteCompletoEntidade> clientes)
        {
            var retorno = new List<ClienteCompleto>();
            foreach (var cliente in clientes)
                retorno.Add(new ClienteCompleto
                {
                    DescricaoCategoria = cliente.DESCRICAO_CATEGORIA,
                    NomeCliente = cliente.NOME
                });
            return retorno;
        } 
        #endregion
    }
}
