﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum & Issues: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

#if EF5 || EF6
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;
#if EF5 || EF6
using System.Data.Entity;

#elif EFCORE
using Microsoft.Data.Entity;

#endif

namespace Z.Test.EntityFramework.Plus
{
    public partial class Audit_GetAudits_ByKey
    {
        [TestMethod]
        public void Key_Single()
        {
            TestContext.DeleteAll(x => x.AuditEntryProperties);
            TestContext.DeleteAll(x => x.AuditEntries);
            TestContext.DeleteAll(x => x.Entity_Guids);

            TestContext.Insert(x => x.Entity_Guids, 3);

            var audit = AuditHelper.AutoSaveAudit();

            Entity_Guid auditedItem = null;

            using (var ctx = new TestContext())
            {
                auditedItem = TestContext.Insert(ctx, x => x.Entity_Guids, 3)[2];
                ctx.SaveChanges(audit);
            }

            // UnitTest - Audit
            {
                using (var ctx = new TestContext())
                {
                    var auditEntries = ctx.AuditEntries.Where<Entity_Guid>(auditedItem.ID).ToList();

                    Assert.AreEqual(1, auditEntries.Count);

                    {
                        var auditEntry = auditEntries[0];
                        Assert.AreEqual(auditedItem.ID.ToString(), auditEntry.Properties.Single(x => x.PropertyName == "ID").NewValueFormatted);
                        Assert.AreEqual(auditedItem.ColumnInt.ToString(), auditEntry.Properties.Single(x => x.PropertyName == "ColumnInt").NewValueFormatted);
                    }
                }
            }
        }
    }
}

#endif