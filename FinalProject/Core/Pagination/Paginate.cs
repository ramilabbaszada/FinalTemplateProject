using System;
using System.Collections;

namespace Core.Pagination
{
    public class Paginate : IPaginate
    {
        public Paginate(int size,int page,int count) { 
            this.Count = count;
            this.Size = size;
            this.Pages = (int)Math.Ceiling(this.Count*1D/this.Size);
            this.Page=page>this.Pages?this.Pages:page;
        }

        public int Page { get; }

        public int Size { get; }

        public int Count { get; }

        public int Pages { get; }

        public bool HasPrevisous => Page > 1;

        public bool HasNext => Page < Pages;

        public IEnumerable Items { get; set; }
    }
}
