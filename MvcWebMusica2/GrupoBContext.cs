﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2;

public partial class GrupoBContext : DbContext
{
    public GrupoBContext()
    {
    }

    public GrupoBContext(DbContextOptions<GrupoBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProximosConcierto> ProximosConciertos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProximosConcierto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProximosConciertos");

            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gira)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Grupo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
