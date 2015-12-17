﻿// Description: EF Bulk Operations & Utilities | Bulk Insert, Update, Delete, Merge from database.
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: http://zzzprojects.uservoice.com/forums/283924-entity-framework-plus
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;

namespace Z.Test.EntityFramework.Plus
{
    public partial class QueryFilter_DbSet_Filter
    {
        [TestMethod]
        public void WithGlobalFilter_ManyFilter_Disabled()
        {
            FilterEntityHelper.Clear();
            FilterEntityHelper.AddTen();

            using (var ctx = new EntityContext(true, enableFilter1: false, enableFilter2: false, enableFilter3: false, enableFilter4: false))
            {
                Assert.AreEqual(35, ctx.FilterEntities.Filter(
                    FilterEntityHelper.Filter.Filter1,
                    FilterEntityHelper.Filter.Filter2,
                    FilterEntityHelper.Filter.Filter3,
                    FilterEntityHelper.Filter.Filter4).Sum(x => x.ColumnInt));
            }
        }
    }
}