﻿using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);

            try
            {
                _taskService.AddTaskForUser(userId, task);
            }
            catch (InvalidUserIdException ex)
            {
                return ex.Message;
            }
            catch (UserNotFoundException ex)
            {
                return ex.Message;
            }
            catch (TaskAlreadyExistsException ex)
            {
                return ex.Message;
            }

            return null;
        }
    }
}