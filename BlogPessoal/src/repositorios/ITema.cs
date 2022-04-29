using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de tema</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface ITema
    {
        void NovoTema(NovoTemaDTO tema);
        void AtualizarTema(AtualizarTemaDTO tema);
        void DeletarTema(int id);
        TemaModelo PegarTemaPeloId(int id);
        List<TemaModelo> PegarTemaPelaDescricao(string descricao);
    }
}
