using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace GlamCentral.Models.Tests
{
    [TestClass()]
    public class ProcedimentoTests
    {
        [TestMethod()]
        public void InsereDuracao_ValoresValidos_ValorInserido()
        {
            var procedimento = Procedimento();
            procedimento.InsereDuracao(25, 10);
            Assert.AreEqual(1510, procedimento.Duracao);
        }

        [TestMethod()]
        public void InsereDuracao_ValoresNegativos_ValorNaoInserido()
        {
            var procedimento = Procedimento();
            procedimento.InsereDuracao(-25, -10);
            Assert.AreEqual(0, procedimento.Duracao);
        }

        [TestMethod()]
        public void InsereDuracao_ValoresInvativos_ValorNaoInserido()
        {
            var procedimento1 = new Mock<Procedimento>();
            procedimento1.SetupGet(_ => _.Duracao).Returns(25);

            var procedimento = procedimento1.Object;
            procedimento.InsereDuracao(-25, -10);
            Assert.AreEqual(25, procedimento.Duracao);
        }

        private Procedimento Procedimento()
        {
            return new Mock<Procedimento>().Object;
        }
    }
}