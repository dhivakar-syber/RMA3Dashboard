using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using SyberGate.RMACT.Friendships.Dto;

namespace SyberGate.RMACT.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}