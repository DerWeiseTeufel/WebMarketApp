using Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Solution: IUndeletable
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public string URL { get; set; } = null!;

        public DateTime UploadDate { get; set; }

        public int TaskItemId { get; set; }

        public bool IsRemoved { get; set; }

        public string ExecutorId { get; set; } = null!;

        public virtual User Executor { get; set; } = null!;

        public virtual TaskItem TaskItem { get; set; } = null!;
    }
}
