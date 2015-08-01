﻿using System;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public class GroupCommandService : ServiceCommandBase, IGroupCommandService
    {
        private readonly IUserService _userService;
        
        public GroupCommandService(DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService) : base(dbContextScopeFactory)
        {
            _userService = userService;
        }

        public void Create(Group input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.CreatedOn = DateTime.Now;
            input.CreatedBy = user.Identity.Name;
            input.IsDeleted = false;

            CreateEntity(input);

            AuditLog.Audit(String.Format("GroupCommandService - Group: {0} - User: {1} - {2}", input.Name, user.Identity.Name, DateTime.Now));
        }
    }

    public interface IGroupCommandService : IServiceCommandBase
    {
        void Create(Group group);
    }
}