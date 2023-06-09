using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class TodoPlusController : ControllerBase
    {
        [HttpGet("/Home")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            return Ok(context.TodoItemsPlus.ToList());
        }

         [HttpGet("/Home/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var todos = context.TodoItemsPlus.FirstOrDefault(x => x.Id == id);
            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        [HttpPost("/Inserir")]
        public IActionResult Post(
        [FromBody] TodoModelPlus model,
        [FromServices] AppDbContext context)
        {
            context.TodoItemsPlus.Add(model);
            context.SaveChanges();
            return Created($"/{model.Id}", model);
        }
           
          [HttpPut("/{id:int}")]
        public IActionResult Put(
        [FromRoute] int id,
        [FromBody] TodoModelPlus todo,
        [FromServices] AppDbContext context)
        {
            var model = context.TodoItemsPlus.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

           model.Title = todo.Title;
           model.Description = todo.Description;
           model.Done =todo.Done;

            context.TodoItemsPlus.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

          [HttpDelete("/deletar/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
        [FromServices] AppDbContext context)
        {
            var modelFromFrontend = context.TodoItemsPlus.FirstOrDefault(x => x.Id == id);
            if (modelFromFrontend == null)
            {
                return NotFound();
            }

            context.TodoItemsPlus.Remove(modelFromFrontend);
            context.SaveChanges();

            return Ok();
        }

        //change done status
        [HttpPut("/done/{id:int}")]
        public IActionResult Done(
        [FromRoute] int id,
        [FromServices] AppDbContext context)
        {
            var model = context.TodoItemsPlus.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

           model.Done = !model.Done;

            context.TodoItemsPlus.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        //change tittle and description
        [HttpPut("/change/{id:int}")]
        public IActionResult Change(
        [FromRoute] int id,
        [FromBody] TodoModelPlus todo,
        [FromServices] AppDbContext context)
        {
            var model = context.TodoItemsPlus.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

           model.Title = todo.Title;
           model.Description = todo.Description;

            context.TodoItemsPlus.Update(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}