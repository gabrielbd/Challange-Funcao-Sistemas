using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BeneficiarioService service = new BeneficiarioService();
            model.CPF = model.CPF.Replace(".", "").Replace("-", "");

            if (!this.ModelState.IsValid)
            {
                var erros = ModelState
                    .Where(kvp => kvp.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                Response.StatusCode = 400;
                return Json(erros);
            }

            if (service.VerificarExistencia(model.CPF))
            {
                var erros = new Dictionary<string, List<string>>();
                erros.Add("CPF", new List<string> { "CPF já cadastrado" });

                Response.StatusCode = 400;
                return Json(erros);
            }
            else
            {
                model.Id = service.Created(new Beneficiario()
                {
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IdCliente = model.IdCliente

                });

                return Json(new { id = model.Id, success = "Cadastro efetuado com sucesso" });
            }
        }

        [HttpGet]
        public ActionResult GetByIdClient(long idClient)
        {
            BeneficiarioService service = new BeneficiarioService();
            List<Beneficiario> beneficiario = service.GetByIdCliente(idClient);

            return Content(JsonConvert.SerializeObject(beneficiario), "application/json");
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            BeneficiarioService service = new BeneficiarioService();
            service.Excluir(id);
            return Json(new { success = "Beneficiário excluído com sucesso" });
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BeneficiarioService benef = new BeneficiarioService();

            if (!this.ModelState.IsValid)
            {
                var erros = ModelState
                    .Where(kvp => kvp.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                Response.StatusCode = 400;
                return Json(erros);
            }
            else
            {
                benef.Update(new Beneficiario()
                {
                    Id = model.Id,
                    Nome = model.Nome,  
                    CPF = model.CPF
                });

                return Json(new { success = "Cadastro alterado com sucesso" });
            }
        }
    }
}