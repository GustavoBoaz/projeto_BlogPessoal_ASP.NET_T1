using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    public class BlogPessoalContexto : DbContext
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; }
        public DbSet<TemaModelo> Temas { get; set; }
        public DbSet<PostagemModelo> Postagens { get; set; }

        public BlogPessoalContexto(DbContextOptions<BlogPessoalContexto> opt) : base(opt)
        {

        }
    }
}
