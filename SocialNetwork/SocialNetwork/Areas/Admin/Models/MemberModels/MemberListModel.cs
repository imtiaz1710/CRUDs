using Autofac;
using SocialNetwork.Models;
using SocialNetwork.ProfileManager.Services;
using System.Linq;

namespace SocialNetwork.Areas.Admin.Models.MemberModels
{
    public class MemberListModel
    {
        private IMemberService _memberService;

        public MemberListModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }

        public MemberListModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        internal object GetMembers(DataTablesAjaxRequestModel tableModel)
        {
            var data = _memberService.GetMembers(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Id", "Name", "DateOfBirth", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Id.ToString(),
                                record.Name,
                                record.DateOfBirth.Date.ToString(),
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _memberService.DeleteMember(id);
        }
    }
}
