﻿using System;
using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                throw new InvalidUserIdException($"Invalid user id: {userId}.", userId);
            }

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new UserNotFoundException($"User {userId} not found.");

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new TaskAlreadyExistsException($"The task already exists.");
            }

            tasks.Add(task);
        }
    }
}