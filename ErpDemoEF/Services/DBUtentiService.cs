using ErpDemoEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErpDemoEF.Services
{
    public interface IDBUTentiService
    {
        public Utenti Login(string utente, string password);
        public void CreaSessione(string utente);
        public void EliminaSessione(string utente);
        public Sessioni SessioneAttiva(string utente);
        public string DocumentoBloccato(string documento, int id);
        public void BloccaDocumento(string utente, string documento, int id);
        public void SbloccaDocumento(string utente, string documento, int id);
    }
    public class DBUtentiService : IDBUTentiService
    {
        public DBUtentiService()
        {

        }
        public Utenti Login(string utente, string password)
        {
            using(var _db = new ErpDemoContext())
            {
                return _db.Utenti
                    .Where(u => u.username == utente && u.password == password)
                    .FirstOrDefault();
            }
        }
        public void CreaSessione(string utente)
        {
            using (var _db = new ErpDemoContext())
            {
                _db.Sessioni.Add(new Sessioni { username = utente, creazione = DateTime.Now });
                _db.SaveChanges();
            }
        }
        public Sessioni SessioneAttiva(string utente)
        {
            using (var _db = new ErpDemoContext())
            {
                return _db.Sessioni.Where(s => s.username == utente).FirstOrDefault();
            }
        }
        public void EliminaSessione(string utente)
        {
            using (var _db = new ErpDemoContext())
            {
                Sessioni se = SessioneAttiva(utente);
                if(se != null)
                {
                    _db.Sessioni.Remove(se);
                    _db.SaveChanges();
                }
            }
        }
        public string DocumentoBloccato(string documento, int id)
        {
            using (var context = new ErpDemoContext())
            {
                SemaforoDocumenti sem = context.SemaforoDocumenti.Where(u => u.doc == documento && u.id == id).FirstOrDefault();
                if (sem != null)
                    return sem.username;
                else
                    return "";
            }
        }
        public void BloccaDocumento(string utente, string documento, int id)
        {
            using (var context = new ErpDemoContext())
            {
                context.SemaforoDocumenti.Add(new SemaforoDocumenti { username = utente, doc = documento, id = id });
                context.SaveChanges();
            }
        }
        public void SbloccaDocumento(string utente, string documento, int id)
        {
            using (var context = new ErpDemoContext())
            {
                SemaforoDocumenti sem = context.SemaforoDocumenti.Where(u => u.username == utente && u.doc == documento && u.id == id).FirstOrDefault();
                if (sem != null)
                {
                    context.SemaforoDocumenti.Remove(sem);
                    context.SaveChanges();
                }
            }
        }
    }
}
