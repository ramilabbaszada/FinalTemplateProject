
namespace Core.Pagination
{
    public abstract class PaginateRequest
    {
        private int page;
        private int size;

        public int Page {
            get => this.page < 1 ? 1 : this.page;
            set => this.page = value;
        }

        public int Size { get => this.size < 1 ? 1 : this.size; set => this.size = value; }

        public string[] Fields { get; set; }

        public PaginateFilter[] Filters { get; set; }
    }
}
