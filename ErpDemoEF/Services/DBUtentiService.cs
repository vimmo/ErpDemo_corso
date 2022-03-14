using ErpDemoEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErpDemoEF.Services
{
    public interface IDBUtentiService
    {
        public Utenti Login(Utenti utente);
        public Sessioni SessioneAttiva(string username);
        public void CreaSessione(string username);
        public void EliminaSessione(string username);
        public string DocumentoBloccato( string documento, int id);
        public void BloccaDocumento(string utente, string documento, int id);
        public void SbloccaDocumento(string utente, string documento, int id);

    }

    public class DBUtentiService : IDBUtentiService
    {
        public DBUtentiService()
        {
        }
        public Utenti Login(Utenti utente)
        {
            using (var context = new ErpDemoContext())
            {
                return context.Utenti.AsNoTracking().Where(u => u.username == utente.username && u.password == utente.password).SingleOrDefault();
            }
        }
        public void CreaSessione(string username)
        {
            using (var context = new ErpDemoContext())
            {
                context.Sessioni.Add(new Sessioni { username = username, creazione = DateTime.Now });
                context.SaveChanges();
            }
        }
        public Sessioni SessioneAttiva(string username)
        {
            using (var context = new ErpDemoContext())
            {
                return context.Sessioni.AsNoTracking().Where(u => u.username == username).SingleOrDefault();
            }
        }
        public void EliminaSessione(string username)
        {
            using (var context = new ErpDemoContext())
            {
                Sessioni se = SessioneAttiva(username);
                if (se != null)
                {
                    context.Sessioni.Remove(se);
                    context.SaveChanges();
                }
            }
        }
        public string DocumentoBloccato( string documento, int id)
        {
            using (var context = new ErpDemoContext())
            {
                SemaforoDocumenti sem = context.SemaforoDocumenti.AsNoTracking().Where(u => u.doc == documento && u.id == id).FirstOrDefault();
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
                SemaforoDocumenti sem = context.SemaforoDocumenti.AsNoTracking().Where(u => u.username == utente && u.doc == documento && u.id == id).FirstOrDefault();
                if (sem != null)
                {
                    context.SemaforoDocumenti.Remove(sem);
                    context.SaveChanges();
                }
            }
        }

    }
}
