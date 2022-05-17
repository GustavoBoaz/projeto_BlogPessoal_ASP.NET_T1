using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IPostagem</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class PostagemRepositorio : IPostagem
    {
        #region Atributos
       
        private readonly BlogPessoalContexto _contexto;
        
        #endregion Atributos

            
        #region Construtores
		
        public PostagemRepositorio(BlogPessoalContexto contexto)
        {
        	_contexto = contexto;
        }
        
        #endregion Construtores

     
        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas postagens</para>
        /// </summary>
        /// <return>Lista PostagemModelo></return>
        public async Task<List<PostagemModelo>> PegarTodasPostagensAsync() 
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma postagem pelo Id</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <return>PostagemModelo</return>
        public async Task<PostagemModelo> PegarPostagemPeloIdAsync(int id) 
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar pegar postagens por pesquisa</para>
        /// </summary>
        /// <param name="tituloPostagem">Titulo da postagem</param>
        /// <param name="descricaoTema">Descrição do tema</param>
        /// <param name="emailCriador">Nome do criador</param>
        /// <return>List PostagemModelo</return>
        public async Task<List<PostagemModelo>> PegarPostagensPorPesquisaAsync(
            string tituloPostagem,
            string descricaoTema,
            string emailCriador)  
        {
            switch (tituloPostagem, descricaoTema, emailCriador)
            {
                case (null, null, null):
                    return await PegarTodasPostagensAsync();

                case (null, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Criador.Email.Contains(emailCriador))
                        .ToListAsync();

                case (null, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (_, null, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Titulo.Contains(tituloPostagem))
                        .ToListAsync();

                case (_, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(tituloPostagem) &
                            p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (null, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Tema.Descricao.Contains(descricaoTema) &
                            p.Criador.Email.Contains(emailCriador))
                        .ToListAsync();

                case (_, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(tituloPostagem) &
                            p.Criador.Email.Contains(emailCriador))
                        .ToListAsync();

                case (_, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(tituloPostagem) |
                            p.Tema.Descricao.Contains(descricaoTema) |
                            p.Criador.Email.Contains(emailCriador))
                        .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        public async Task NovaPostagemAsync(NovaPostagemDTO postagem)
        {
            await _contexto.Postagens.AddAsync(new PostagemModelo
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email == postagem.EmailCriador),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema)
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma postagem</para>
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        public async Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem)  
        {
            var postagemExistente = await PegarPostagemPeloIdAsync(postagem.Id);
            postagemExistente.Titulo = postagem.Titulo;
            postagemExistente.Descricao = postagem.Descricao;
            postagemExistente.Foto = postagem.Foto;
            postagemExistente.Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema);

            _contexto.Postagens.Update(postagemExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma postagem</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        public async Task DeletarPostagemAsync(int id)   
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
            
        #endregion Métodos
    }
}