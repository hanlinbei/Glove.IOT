//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Glove.IOT.Model
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class R_RoleInfo_ActionInfo
    {
        public R_RoleInfo_ActionInfo()
        {
            this.StatusFlag = 1;
        }
    
        public int Id { get; set; }
        public int RoleInfoId { get; set; }
        public int ActionInfoId { get; set; }
        public short StatusFlag { get; set; }
    
        public virtual RoleInfo RoleInfo { get; set; }
        public virtual ActionInfo ActionInfo { get; set; }
    }
}
