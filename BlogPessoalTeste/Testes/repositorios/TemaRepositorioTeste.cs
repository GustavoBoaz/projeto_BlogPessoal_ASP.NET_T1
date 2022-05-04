using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BlogPessoalTeste.Testes.repositorios
{
    
    [TestClass]
    public class TemaRepositorioTeste
    {

        private BlogPessoalContexto _contexto;
        private ITema _repositorio;
        
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);
        }
        
        [TestMethod]
        public void CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            //GIVEN - Dado que registro 4 temas no banco
            _repositorio.NovoTema(new NovoTemaDTO("C#"));
            _repositorio.NovoTema(new NovoTemaDTO("Java"));
            _repositorio.NovoTema(new NovoTemaDTO("Python"));
            _repositorio.NovoTema(new NovoTemaDTO("JavaScript"));
            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, _repositorio.PegarTodosTemas().Count);
        }
        
        [TestMethod]
        [DataRow(1)]
        public void PegarTemaPeloIdRetornaTema1(int id)
        {
            //GIVEN - Dado que pesquiso pelo id 1
            var tema = _repositorio.PegarTemaPeloId(id);
            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Descricao);
        }
        
        [TestMethod]
        [DataRow("Java")]
        public void PegaTemaPelaDescricaoRetornadoisTemas(string descricao)
        {
            //GIVEN - Dado que pesquiso pela descricao Java
            var temas = _repositorio.PegarTemaPelaDescricao(descricao);
            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }
        
        [TestMethod]
        [DataRow(3)]
        public void AlterarTemaPythonRetornaTemaCobol(int id)
        {
            //GIVEN - Dado que passo o Id 3 e o tema COBOL
            _repositorio.AtualizarTema(new AtualizarTemaDTO(id, "COBOL"));
            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL",
            _repositorio.PegarTemaPeloId(id).Descricao);
        }
        
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void DeletarTemasRetornaNulo(int id)
        {
            //GIVEN - Dado que passo o Id 1, 2, 3, 4
            _repositorio.DeletarTema(id);
            //THEN - Entao deve retornar nulo

            Assert.IsNull(_repositorio.PegarTemaPeloId(id));
        }
    }
}