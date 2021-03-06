﻿using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api.Services
{
    public interface IBlockingEntityService : IEntityService<Blocking, BlockingData> { }
    public class BlockingEntityService : IBlockingEntityService
    {
        public Blocking Map(BlockingData ent) => new Blocking(ent.LockType, ent.Database, ent.BlockObject,
                ent.LockRequest, ent.WaiterSid, ent.WaitTime, ent.WaiterBatch,
                ent.WaiterStatement, ent.BlockerSid, ent.BlockerBatch);

        public BlockingData Map(Blocking ent) => throw new NotImplementedException();
    }
}
