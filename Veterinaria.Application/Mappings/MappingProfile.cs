

using AutoMapper;
using Veterinaria.Application.DTOs.Dueno;
using Veterinaria.Application.DTOs.Mascota;
using Veterinaria.Application.DTOs.Usuario;
using Veterinaria.Application.DTOs.Veterinario;
using Veterinaria.Application.DTOs.Cita;
using Veterinaria.Domain.Entities;


namespace Veterinaria.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Mapeo de Usuairo
            CreateMap<ApplicationUser, UsuarioDto>()
            .ForMember(dest => dest.Rol, opt => opt.Ignore());

            CreateMap<UsuarioRegistroDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            #endregion

            #region Mapeo de Duenos
            CreateMap<Dueno, DuenoDto>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Usuario.NombreCompleto));
            CreateMap<DuenoCrearDto, Dueno>();
            CreateMap<DuenoActualizarDto, Dueno>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            #endregion

            #region mapeo de mascota
            CreateMap<Mascota, MascotaDto>()
              .ForMember(dest => dest.NombreDueno,
                  opt => opt.MapFrom(src => src.Dueno.NombreDueno));
            CreateMap<MascotaCrearDto, Mascota>();
            CreateMap<MascotaActualizarDto, Mascota>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            #endregion

            #region Mapeo de Veterinarios
            CreateMap<Veterinario, VeterinarioDto>()
            .ForMember(dest => dest.NombreCompleto,
            opt => opt.MapFrom(src => src.Usuario.NombreCompleto));
            CreateMap<VeterianrioCrearDto, Veterinario>();
            CreateMap<VeterinarioActualizarDto, Veterinario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdUsuario, opt => opt.Ignore());
                //.ForMember(dest => dest.FechaRegistro, opt => opt.Ignore());
            #endregion

            #region Mapeo de Citas
            CreateMap<Cita, CitaDto>()
                .ForMember(dest => dest.NombreDueno, opt => opt.MapFrom(src => src.Dueno.NombreDueno))
                .ForMember(dest => dest.NombreVeterinario, opt => opt.MapFrom(src => src.Veterinario.NombreVeterinario))
                .ForMember(dest => dest.FechaCita, opt => opt.MapFrom(src => src.FechaCita.ToString("dd/MM/yyyy hh:mmtt").ToLower()));
            CreateMap<CitaCrearDto, Cita>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.Dueno, opt => opt.Ignore())
                .ForMember(dest => dest.Veterinario, opt => opt.Ignore());

            CreateMap<DetallesCita, DetallesCitaDto>();
            CreateMap<DetallesCitaCrearDto, DetallesCita>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CitaID, opt => opt.Ignore())
                .ForMember(dest => dest.Cita, opt => opt.Ignore())
                .ForMember(dest => dest.Mascota, opt => opt.Ignore())
                .ForMember(dest => dest.NombreMascota, opt => opt.Ignore());
            #endregion

        }

    }
}
