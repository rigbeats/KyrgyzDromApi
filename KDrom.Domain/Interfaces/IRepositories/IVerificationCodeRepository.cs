﻿using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IVerificationCodeRepository
{
    Task AddAsync(VerificationCode verificationCode);

    Task<VerificationCode?> GetByUserIdAsync(Guid id);

    Task SaveChangesAsync();
}
