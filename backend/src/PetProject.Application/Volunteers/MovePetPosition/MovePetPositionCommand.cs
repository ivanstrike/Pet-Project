﻿namespace PetProject.Application.Volunteers.MovePetPosition;

public record MovePetPositionCommand(Guid VolunteerId, Guid PetId, int Position);