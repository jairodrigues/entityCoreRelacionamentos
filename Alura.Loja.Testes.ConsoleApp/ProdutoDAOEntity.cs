using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class ProdutoDAOEntity : IProdutoDAO
    {
        private LojaContext _contexto;

        public ProdutoDAOEntity(LojaContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Produto p)
        {
            _contexto.Produtos.Add(p);
            _contexto.SaveChanges();
        }

        public void Atualizar(Produto p)
        {
            _contexto.Produtos.Update(p);
            _contexto.SaveChanges();
        }

        public IList<Produto> Produtos()
        {
           return _contexto.Produtos.ToList();
        }

        public void Remover(Produto p)
        {
            _contexto.Produtos.Remove(p);
            _contexto.SaveChanges();
        }
    }
}
