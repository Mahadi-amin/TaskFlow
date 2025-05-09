﻿using TaskFlow.Application.ServicesInterface;
using TaskFlow.Domain;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly ITaskFlowUnitOfWork _taskFlowUnitOfWork;

        public StatusService(ITaskFlowUnitOfWork taskFlowUnitOfWork)
        {
            _taskFlowUnitOfWork = taskFlowUnitOfWork;
        }

        public IList<Status> GetAllStatus()
        {
            return _taskFlowUnitOfWork.StatusRepository.GetAll();
        }

        public (IList<Status> data, int total, int totalDisplay) GetStatus(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _taskFlowUnitOfWork.StatusRepository.GetPagedStatus(pageIndex, pageSize, search, order);
        }

    }
}
