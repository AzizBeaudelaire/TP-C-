using System.Collections.Generic;
using Web.Business;
using System.Linq;
using Web.Business.Models;

namespace Web.Business.IServices
{
    public interface ITodoService
    {
        IEnumerable<PostGroup> GetTodoBoard();
        PostGroup GetTodoById(int id);
        PostGroup AddTodo(PostGroup model);
        string UpdateTask(int taskId, PostGroup updatedPost);
        string DeleteTask(int taskId);
    }
}