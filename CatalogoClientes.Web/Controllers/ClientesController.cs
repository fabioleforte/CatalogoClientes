using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatalogoClientes.Dominio.Entidades;
using CatalogoClientes.Dominio.Repositorio;
using PagedList;
using System.IO;

namespace CatalogoClientes.Web.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteContexto db = new ClienteContexto();

        private IRepositorio<Cliente> _repositorioCliente;

        public ClientesController()
        {
            _repositorioCliente = new ClientesRepositorio(new ClienteContexto());
        }

        public ActionResult Catalogo(int? pagina)
        {
            int tamanhoPagina = 1;
            int numeroPagina = pagina ?? 1;
            return View(_repositorioCliente.GetTodos().ToPagedList(numeroPagina, tamanhoPagina));
        }

        //// GET: Clientes
        //public ActionResult Catalago()
        //{
        //    return View(db.Clientes.ToList());
        //}

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nome,Email,Endereco,Imagem,ImagemTipo")] Cliente cliente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Cliente
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);

                    }
                    cliente.Imagem = arqImagem.Imagem;
                    cliente.ImagemTipo = arqImagem.ImagemTipo;
                }
                db.Clientes.Add(cliente);
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0} : foi incluído com sucesso!!!", cliente.Nome);
                return RedirectToAction("Catalogo");
            }
            return View(cliente);
        }
        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Email,Endereco,Imagem,ImagemTipo")] Cliente cliente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Cliente
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);

                    }
                    cliente.Imagem = arqImagem.Imagem;
                    cliente.ImagemTipo = arqImagem.ImagemTipo;
                }
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0} : foi atualizado com sucesso!!!", cliente.Nome);
                return RedirectToAction("Catalogo");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            TempData["mensagem"] = string.Format("{0} : foi excluído com sucesso!!!", cliente.Nome);
            return RedirectToAction("Catalogo");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
