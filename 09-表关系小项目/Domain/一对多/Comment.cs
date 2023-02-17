using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.一对多
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Article TheArticle { get; set; }

        public int ArticleId { get; set; }
    }
}
