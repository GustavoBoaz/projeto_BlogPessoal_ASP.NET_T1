using System.Linq;
using System.Threading.Tasks;
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
        
        [TestMethod]
        public async Task CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro 4 temas no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Java"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Python"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("JavaScript"));

            var temas = await _repositorio.PegarTodosTemasAsync();

            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, temas.Count);
        }

        [TestMethod]
        public async Task PegarTemaPeloIdRetornaTema1()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro C# no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var tema = await _repositorio.PegarTemaPeloIdAsync(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Descricao);
        }
        
        [TestMethod]
        public async Task PegaTemaPelaDescricaoRetornadoisTemas()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Java no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Java"));
            //AND - E que registro JavaScript no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("JavaScript"));

            //WHEN - Quando que pesquiso pela descricao Java
            var temas = await _repositorio.PegarTemasPelaDescricaoAsync("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }
        
        [TestMethod]
        public async Task AlterarTemaPythonRetornaTemaCobol()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Python no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Python"));

            //WHEN - Quando passo o Id 1 e a descricao COBOL
            await _repositorio.AtualizarTemaAsync(new AtualizarTemaDTO(1, "COBOL"));

            var tema = await _repositorio.PegarTemaPeloIdAsync(1);

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", tema.Descricao);
        }
        
        [TestMethod]
        public async Task DeletarTemasRetornaNulo()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro 1 temas no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));

            //WHEN - quando deleto o Id 1
            await _repositorio.DeletarTemaAsync(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(await _repositorio.PegarTemaPeloIdAsync(1));
        }
    }
}