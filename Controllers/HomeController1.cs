using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController1 : ControllerBase
    {
        [HttpGet("/")]//metodo Lista de ToDos
        public List<TodoModel> Get(
            [FromServices] AppDbContext context
            )
        {
            return context.Todos.ToList();
        }
        [HttpGet("/{id:int}")]//Metodo que só retorna 1 todo
        public TodoModel Get(
            int id,
           [FromServices] AppDbContext context
           )
        {
            return context.Todos.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("/")]//asp net agora sabe que é get
    
        public TodoModel Post(
         [FromBody] TodoModel todo,
           [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }

        [HttpPut("/{id:int}")]
        public TodoModel Put(
            int id,
        [FromBody] TodoModel todo,  //valor da tela postma,
          [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x=> x.Id == id);
            if(model == null)
            {
                return todo;
            }

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return model;

        }

        [HttpDelete("/")]
        public TodoModel Delete(
         int id,
     [FromBody] TodoModel todo,  //valor da tela postma,
       [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
         
            context.Todos.Remove(model);
            context.SaveChanges();

            return model;

        }

    }
}
