using Microsoft.AspNetCore.Http;

namespace GlamCentral.Libraries.Session
{
    public class Sessao
    {
        #region "Propriedades Privadas"
        IHttpContextAccessor _context;
        #endregion

        #region "Construtor"
        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(string key, string value)
            => _context.HttpContext.Session.SetString(key, value);

        public void Atualizar(string key, string value)
        {
            if (Existe(key))
                _context.HttpContext.Session.Remove(key);
            _context.HttpContext.Session.SetString(key, value);
        }

        public void Remover(string key)
        {
            _context.HttpContext.Session.Remove(key);
        }

        public string Consultar(string key) 
            => _context.HttpContext.Session.GetString(key);

        public bool Existe(string key)
        {
            if (_context.HttpContext.Session.GetString(key) == null)
                return false;
            return true;
        }

        public void RemoverTodos() 
            => _context.HttpContext.Session.Clear();
        #endregion
    }
}
