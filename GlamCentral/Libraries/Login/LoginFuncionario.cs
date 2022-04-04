using GlamCentral.Libraries.Session;
using GlamCentral.Models;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GlamCentral.Libraries.Login
{
    public class LoginFuncionario
    {
        #region "Propriedades Pùblicas"
        private string _key = "Login.Funcionario";
        private Sessao _sessao;
        #endregion

        #region "Construtor"
        public LoginFuncionario(Sessao sessao)
        {
            _sessao = sessao;
        }
        #endregion

        #region "Métodos Públicos"
        public void Login(Funcionario funcionario)
        {
            if (funcionario.Status == true)
                _sessao.Cadastrar(_key, JsonConvert.SerializeObject(funcionario));
        }

        public Funcionario GetFuncionario()
        {
            if(_sessao.Existe(_key))
             return JsonConvert.DeserializeObject<Funcionario>(_sessao.Consultar(_key));
            return null;
        }

        public void Logout() 
            => _sessao.RemoverTodos();

        public string GerarHashMd5(string entrada)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Converter a String para array de bytes, que é como a biblioteca trabalha.
                byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(entrada));

                // Cria-se um StringBuilder para recompôr a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop para formatar cada byte como uma String em hexadecimal
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
        #endregion
    }
}
