using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using BlogPessoal.src.utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class PostagemRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorioU;
        private ITema _repositorioT;
        private IPostagem _repositorioP;

        [TestMethod]
        public async Task CriaTresPostagemNoSistemaRetornaTres()
        {
            // Definindo o contexto
           var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 2 usuarios
            await _repositorioU.NovoUsuarioAsync(
                new NovoUsuarioDTO("Gustavo Boaz","gustavo@email.com","134652", "URLDAFOTO", TipoUsuario.NORMAL)
            );
            
            await _repositorioU.NovoUsuarioAsync(
                new NovoUsuarioDTO("Catarina Boaz","catarina@email.com","134652","URLDAFOTO", TipoUsuario.NORMAL)
            );
            
            // AND - E que registro 2 temas
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("C#"));
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );
            
            // WHEN - Quando eu busco todas as postagens
            var postagens = await _repositorioP.PegarTodasPostagensAsync();

            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, postagens.Count());
        }

        [TestMethod]
        public async Task AtualizarPostagemRetornaPostagemAtualizada()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 1 usuarios
            await _repositorioU.NovoUsuarioAsync(
                new NovoUsuarioDTO("Gustavo Boaz","gustavo@email.com","134652", "URLDAFOTO", TipoUsuario.NORMAL)
            );
            
            // AND - E que registro 1 tema
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("COBOL"));
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("C#"));

            // AND - E que registro 1 postagem
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            await _repositorioP.AtualizarPostagemAsync(
                new AtualizarPostagemDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            var postagem = await _repositorioP.PegarPostagemPeloIdAsync(1);

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual("C# é muito massa", postagem.Titulo);
            Assert.AreEqual("C# é muito utilizada no mundo", postagem.Descricao);
            Assert.AreEqual("URLDAFOTOATUALIZADA", postagem.Foto);
            Assert.AreEqual("C#", postagem.Tema.Descricao);
        }

        [TestMethod]
        public async Task PegarPostagensPorPesquisaRetodarCustomizada()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 2 usuarios
            await _repositorioU.NovoUsuarioAsync(
                new NovoUsuarioDTO("Gustavo Boaz","gustavo@email.com","134652", "URLDAFOTO", TipoUsuario.NORMAL)
            );
            
            await _repositorioU.NovoUsuarioAsync(
                new NovoUsuarioDTO("Catarina Boaz","catarina@email.com","134652","URLDAFOTO", TipoUsuario.NORMAL)
            );
            
            // AND - E que registro 2 temas
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("C#"));
            await _repositorioT.NovoTemaAsync(new NovoTemaDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositorioP.NovaPostagemAsync(
                new NovaPostagemDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            var postagensTeste1 = await _repositorioP.PegarPostagensPorPesquisaAsync("massa", null, null);
            var postagensTeste2 = await _repositorioP.PegarPostagensPorPesquisaAsync(null, "C#", null);
            var postagensTeste3 = await _repositorioP.PegarPostagensPorPesquisaAsync(null, null, "gustavo@email.com");

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(2, postagensTeste1.Count);
            Assert.AreEqual(2, postagensTeste2.Count);
            Assert.AreEqual(2, postagensTeste3.Count);
        }
    }
}