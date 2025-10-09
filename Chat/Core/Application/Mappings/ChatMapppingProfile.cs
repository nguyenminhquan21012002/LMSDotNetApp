using AutoMapper;
using Chat.Core.Application.DTOs.Messages;
using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Application.DTOs;
using Chat.Core.Domain.Entities;

namespace Chat.Core.Application.Mappings
{
    public class ChatMapppingProfile : Profile
    {
        public ChatMapppingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>().ReverseMap();

            // Conversation mappings
            CreateMap<Conversation, ConversationDto>()
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.UserConversations.Select(uc => uc.User)))
                .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));
            
            // Message mappings
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderDto, opt => opt.MapFrom(src => src.Sender))
                .ForMember(dest => dest.ConversationDto, opt => opt.MapFrom(src => src.Conversation));
            
            CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.SenderDto))
                .ForMember(dest => dest.Conversation, opt => opt.MapFrom(src => src.ConversationDto));

            // UserConversation mappings
            CreateMap<UserConversation, UserConversation>().ReverseMap();
        }
    }
}
