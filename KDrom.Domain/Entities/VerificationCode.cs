﻿using System.ComponentModel.DataAnnotations;

namespace KDrom.Domain.Entities;

public class VerificationCode : EntityBase
{
    public DateTime ExpiredAt { get; set; }

    [MaxLength(6)]
    public string Code { get; set; }

    public bool IsUsed { get; set; }

    [MaxLength(36)]
    public Guid UserId { get; set; }

    public User User { get; set; }
}