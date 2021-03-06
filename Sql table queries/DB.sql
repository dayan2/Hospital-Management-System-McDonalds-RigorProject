USE [LP_HMSDb]
GO
/****** Object:  Table [dbo].[Bed]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bed](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WardId] [int] NULL,
	[BedTicketNo] [varchar](20) NULL,
 CONSTRAINT [PK_Bed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DoctorSpecialityId] [int] NOT NULL,
	[Charges] [decimal](18, 2) NULL,
	[PhoneNo] [varchar](50) NULL,
	[UserId] [int] NOT NULL,
	[WardId] [int] NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DoctorRecomendation]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DoctorRecomendation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecomendedDoctorId] [int] NULL,
	[CurrentDoctorId] [int] NULL,
	[Reason] [varchar](150) NULL,
	[PatientId] [int] NULL,
 CONSTRAINT [PK_DoctorRecomendation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DoctorSpeciality]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DoctorSpeciality](
	[DoctorSpecialityId] [int] IDENTITY(1,1) NOT NULL,
	[SpecializeArea] [varchar](50) NULL,
 CONSTRAINT [PK_DoctorSpeciatly] PRIMARY KEY CLUSTERED 
(
	[DoctorSpecialityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientDetailId] [int] NULL,
	[InvoiceDate] [date] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicalTestType]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MedicalTestType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Medical_Test_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Nurse]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Nurse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[WardId] [int] NULL,
 CONSTRAINT [PK_Nurse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[NIC] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Gender] [char](10) NULL,
	[MobileNo] [varchar](20) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientDetail]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDetail](
	[PatientDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AdmitDate] [date] NULL,
	[BedId] [int] NULL,
	[WardId] [int] NULL,
	[DoctorId] [int] NULL,
	[PatientId] [int] NULL,
	[IsDischarged] [bit] NULL,
 CONSTRAINT [PK_Patient_Detail] PRIMARY KEY CLUSTERED 
(
	[PatientDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientFeedback]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PatientFeedback](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NULL,
	[PatientDetailId] [int] NOT NULL,
	[FeedbackDate] [date] NULL,
	[FeedbackDescription] [varchar](150) NULL,
 CONSTRAINT [PK_PatientFeedback] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientMedicalTest]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientMedicalTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientDetailId] [int] NOT NULL,
	[MedicalTestId] [int] NULL,
	[NurseId] [int] NOT NULL,
 CONSTRAINT [PK_Patient_Medical_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserRoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](20) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ward]    Script Date: 3/18/2015 2:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ward](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WardNo] [varchar](10) NULL,
	[WardFee] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Ward] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Bed]  WITH CHECK ADD  CONSTRAINT [FK_Bed_Ward] FOREIGN KEY([WardId])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Bed] CHECK CONSTRAINT [FK_Bed_Ward]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_DoctorSpeciality] FOREIGN KEY([DoctorSpecialityId])
REFERENCES [dbo].[DoctorSpeciality] ([DoctorSpecialityId])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_DoctorSpeciality]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_User]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Ward] FOREIGN KEY([WardId])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Ward]
GO
ALTER TABLE [dbo].[DoctorRecomendation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRecomendation_Doctor] FOREIGN KEY([RecomendedDoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[DoctorRecomendation] CHECK CONSTRAINT [FK_DoctorRecomendation_Doctor]
GO
ALTER TABLE [dbo].[DoctorRecomendation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRecomendation_Doctor1] FOREIGN KEY([CurrentDoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[DoctorRecomendation] CHECK CONSTRAINT [FK_DoctorRecomendation_Doctor1]
GO
ALTER TABLE [dbo].[DoctorRecomendation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRecomendation_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[DoctorRecomendation] CHECK CONSTRAINT [FK_DoctorRecomendation_Patient]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Patient_Detail] FOREIGN KEY([PatientDetailId])
REFERENCES [dbo].[PatientDetail] ([PatientDetailId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Patient_Detail]
GO
ALTER TABLE [dbo].[Nurse]  WITH CHECK ADD  CONSTRAINT [FK_Nurse_Ward] FOREIGN KEY([WardId])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[Nurse] CHECK CONSTRAINT [FK_Nurse_Ward]
GO
ALTER TABLE [dbo].[PatientDetail]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Detail_Bed] FOREIGN KEY([BedId])
REFERENCES [dbo].[Bed] ([Id])
GO
ALTER TABLE [dbo].[PatientDetail] CHECK CONSTRAINT [FK_Patient_Detail_Bed]
GO
ALTER TABLE [dbo].[PatientDetail]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Detail_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[PatientDetail] CHECK CONSTRAINT [FK_Patient_Detail_Doctor]
GO
ALTER TABLE [dbo].[PatientDetail]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Detail_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[PatientDetail] CHECK CONSTRAINT [FK_Patient_Detail_Patient]
GO
ALTER TABLE [dbo].[PatientDetail]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Detail_Ward] FOREIGN KEY([WardId])
REFERENCES [dbo].[Ward] ([Id])
GO
ALTER TABLE [dbo].[PatientDetail] CHECK CONSTRAINT [FK_Patient_Detail_Ward]
GO
ALTER TABLE [dbo].[PatientFeedback]  WITH CHECK ADD  CONSTRAINT [FK_PatientFeedback_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[PatientFeedback] CHECK CONSTRAINT [FK_PatientFeedback_Doctor]
GO
ALTER TABLE [dbo].[PatientFeedback]  WITH CHECK ADD  CONSTRAINT [FK_PatientFeedback_Patient_Detail] FOREIGN KEY([PatientDetailId])
REFERENCES [dbo].[PatientDetail] ([PatientDetailId])
GO
ALTER TABLE [dbo].[PatientFeedback] CHECK CONSTRAINT [FK_PatientFeedback_Patient_Detail]
GO
ALTER TABLE [dbo].[PatientMedicalTest]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Medical_Test_Medical_Test_Type] FOREIGN KEY([MedicalTestId])
REFERENCES [dbo].[MedicalTestType] ([Id])
GO
ALTER TABLE [dbo].[PatientMedicalTest] CHECK CONSTRAINT [FK_Patient_Medical_Test_Medical_Test_Type]
GO
ALTER TABLE [dbo].[PatientMedicalTest]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Medical_Test_Nurse] FOREIGN KEY([NurseId])
REFERENCES [dbo].[Nurse] ([Id])
GO
ALTER TABLE [dbo].[PatientMedicalTest] CHECK CONSTRAINT [FK_Patient_Medical_Test_Nurse]
GO
ALTER TABLE [dbo].[PatientMedicalTest]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Medical_Test_Patient_Detail] FOREIGN KEY([PatientDetailId])
REFERENCES [dbo].[PatientDetail] ([PatientDetailId])
GO
ALTER TABLE [dbo].[PatientMedicalTest] CHECK CONSTRAINT [FK_Patient_Medical_Test_Patient_Detail]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO
