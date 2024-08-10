﻿using MediatR;

namespace KDrom.Application.Auth.Register;

public class RegisterUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
}
