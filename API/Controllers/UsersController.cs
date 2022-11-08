using Aplication.Dtos;
using Aplication.Users;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController: BaseApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUser.CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    
    
    
    
    
    
    
    
    
    
    
    

}