﻿#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = 
{
	{ 23881, 0,  4 } /*tableIndex: 0 */,
};
#else
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = { { 0, 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const char* g_methodExecutionContextInfoStrings[1] = 
{
	"particle",
};
#else
static const char* g_methodExecutionContextInfoStrings[1] = { NULL };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[70] = 
{
	{ 0, 0 } /* 0x06000001 System.Void UnityEngine.ParticleSystem::set_time(System.Single) */,
	{ 0, 0 } /* 0x06000002 System.Single UnityEngine.ParticleSystem::GetParticleCurrentSize(UnityEngine.ParticleSystem/Particle&) */,
	{ 0, 0 } /* 0x06000003 UnityEngine.Color32 UnityEngine.ParticleSystem::GetParticleCurrentColor(UnityEngine.ParticleSystem/Particle&) */,
	{ 0, 0 } /* 0x06000004 System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[],System.Int32,System.Int32) */,
	{ 0, 0 } /* 0x06000005 System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[],System.Int32) */,
	{ 0, 0 } /* 0x06000006 System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[]) */,
	{ 0, 0 } /* 0x06000007 System.Void UnityEngine.ParticleSystem::Simulate(System.Single,System.Boolean,System.Boolean,System.Boolean) */,
	{ 0, 0 } /* 0x06000008 System.Void UnityEngine.ParticleSystem::Play(System.Boolean) */,
	{ 0, 0 } /* 0x06000009 System.Void UnityEngine.ParticleSystem::Play() */,
	{ 0, 0 } /* 0x0600000A System.Void UnityEngine.ParticleSystem::Stop(System.Boolean,UnityEngine.ParticleSystemStopBehavior) */,
	{ 0, 0 } /* 0x0600000B System.Void UnityEngine.ParticleSystem::Emit(System.Int32) */,
	{ 0, 0 } /* 0x0600000C System.Void UnityEngine.ParticleSystem::Emit_Internal(System.Int32) */,
	{ 0, 0 } /* 0x0600000D System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.ParticleSystem/EmitParams,System.Int32) */,
	{ 0, 0 } /* 0x0600000E System.Void UnityEngine.ParticleSystem::EmitOld_Internal(UnityEngine.ParticleSystem/Particle&) */,
	{ 0, 0 } /* 0x0600000F UnityEngine.ParticleSystem/MainModule UnityEngine.ParticleSystem::get_main() */,
	{ 0, 0 } /* 0x06000010 UnityEngine.ParticleSystem/TextureSheetAnimationModule UnityEngine.ParticleSystem::get_textureSheetAnimation() */,
	{ 0, 1 } /* 0x06000011 System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.Vector3,UnityEngine.Vector3,System.Single,System.Single,UnityEngine.Color32) */,
	{ 0, 0 } /* 0x06000012 System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.ParticleSystem/Particle) */,
	{ 0, 0 } /* 0x06000013 System.Void UnityEngine.ParticleSystem::GetParticleCurrentColor_Injected(UnityEngine.ParticleSystem/Particle&,UnityEngine.Color32&) */,
	{ 0, 0 } /* 0x06000014 System.Void UnityEngine.ParticleSystem::Emit_Injected(UnityEngine.ParticleSystem/EmitParams&,System.Int32) */,
	{ 0, 0 } /* 0x06000015 System.Void UnityEngine.ParticleSystem/MainModule::.ctor(UnityEngine.ParticleSystem) */,
	{ 0, 0 } /* 0x06000016 UnityEngine.ParticleSystemSimulationSpace UnityEngine.ParticleSystem/MainModule::get_simulationSpace() */,
	{ 0, 0 } /* 0x06000017 UnityEngine.ParticleSystemScalingMode UnityEngine.ParticleSystem/MainModule::get_scalingMode() */,
	{ 0, 0 } /* 0x06000018 System.Void UnityEngine.ParticleSystem/MainModule::set_scalingMode(UnityEngine.ParticleSystemScalingMode) */,
	{ 0, 0 } /* 0x06000019 System.Boolean UnityEngine.ParticleSystem/MainModule::get_playOnAwake() */,
	{ 0, 0 } /* 0x0600001A System.Int32 UnityEngine.ParticleSystem/MainModule::get_maxParticles() */,
	{ 0, 0 } /* 0x0600001B System.Void UnityEngine.ParticleSystem/MainModule::set_maxParticles(System.Int32) */,
	{ 0, 0 } /* 0x0600001C UnityEngine.ParticleSystemSimulationSpace UnityEngine.ParticleSystem/MainModule::get_simulationSpace_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0 } /* 0x0600001D UnityEngine.ParticleSystemScalingMode UnityEngine.ParticleSystem/MainModule::get_scalingMode_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0 } /* 0x0600001E System.Void UnityEngine.ParticleSystem/MainModule::set_scalingMode_Injected(UnityEngine.ParticleSystem/MainModule&,UnityEngine.ParticleSystemScalingMode) */,
	{ 0, 0 } /* 0x0600001F System.Boolean UnityEngine.ParticleSystem/MainModule::get_playOnAwake_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0 } /* 0x06000020 System.Int32 UnityEngine.ParticleSystem/MainModule::get_maxParticles_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0 } /* 0x06000021 System.Void UnityEngine.ParticleSystem/MainModule::set_maxParticles_Injected(UnityEngine.ParticleSystem/MainModule&,System.Int32) */,
	{ 0, 0 } /* 0x06000022 System.Void UnityEngine.ParticleSystem/TextureSheetAnimationModule::.ctor(UnityEngine.ParticleSystem) */,
	{ 0, 0 } /* 0x06000023 System.Boolean UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_enabled() */,
	{ 0, 0 } /* 0x06000024 System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesX() */,
	{ 0, 0 } /* 0x06000025 System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesY() */,
	{ 0, 0 } /* 0x06000026 UnityEngine.ParticleSystemAnimationType UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_animation() */,
	{ 0, 0 } /* 0x06000027 UnityEngine.ParticleSystem/MinMaxCurve UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_frameOverTime() */,
	{ 0, 0 } /* 0x06000028 System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_cycleCount() */,
	{ 0, 0 } /* 0x06000029 System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_rowIndex() */,
	{ 0, 0 } /* 0x0600002A System.Boolean UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_enabled_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x0600002B System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesX_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x0600002C System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesY_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x0600002D UnityEngine.ParticleSystemAnimationType UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_animation_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x0600002E System.Void UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_frameOverTime_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&,UnityEngine.ParticleSystem/MinMaxCurve&) */,
	{ 0, 0 } /* 0x0600002F System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_cycleCount_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x06000030 System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_rowIndex_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0 } /* 0x06000031 UnityEngine.AnimationCurve UnityEngine.ParticleSystem/MinMaxCurve::get_curveMin() */,
	{ 0, 0 } /* 0x06000032 System.Single UnityEngine.ParticleSystem/MinMaxCurve::get_constant() */,
	{ 0, 0 } /* 0x06000033 UnityEngine.AnimationCurve UnityEngine.ParticleSystem/MinMaxCurve::get_curve() */,
	{ 0, 0 } /* 0x06000034 UnityEngine.Vector3 UnityEngine.ParticleSystem/Particle::get_position() */,
	{ 0, 0 } /* 0x06000035 System.Void UnityEngine.ParticleSystem/Particle::set_position(UnityEngine.Vector3) */,
	{ 0, 0 } /* 0x06000036 System.Void UnityEngine.ParticleSystem/Particle::set_velocity(UnityEngine.Vector3) */,
	{ 0, 0 } /* 0x06000037 System.Single UnityEngine.ParticleSystem/Particle::get_remainingLifetime() */,
	{ 0, 0 } /* 0x06000038 System.Void UnityEngine.ParticleSystem/Particle::set_remainingLifetime(System.Single) */,
	{ 0, 0 } /* 0x06000039 System.Single UnityEngine.ParticleSystem/Particle::get_startLifetime() */,
	{ 0, 0 } /* 0x0600003A System.Void UnityEngine.ParticleSystem/Particle::set_startLifetime(System.Single) */,
	{ 0, 0 } /* 0x0600003B System.Void UnityEngine.ParticleSystem/Particle::set_startColor(UnityEngine.Color32) */,
	{ 0, 0 } /* 0x0600003C System.UInt32 UnityEngine.ParticleSystem/Particle::get_randomSeed() */,
	{ 0, 0 } /* 0x0600003D System.Void UnityEngine.ParticleSystem/Particle::set_randomSeed(System.UInt32) */,
	{ 0, 0 } /* 0x0600003E System.Void UnityEngine.ParticleSystem/Particle::set_startSize(System.Single) */,
	{ 0, 0 } /* 0x0600003F System.Single UnityEngine.ParticleSystem/Particle::get_rotation() */,
	{ 0, 0 } /* 0x06000040 UnityEngine.Vector3 UnityEngine.ParticleSystem/Particle::get_rotation3D() */,
	{ 0, 0 } /* 0x06000041 System.Void UnityEngine.ParticleSystem/Particle::set_rotation3D(UnityEngine.Vector3) */,
	{ 0, 0 } /* 0x06000042 System.Void UnityEngine.ParticleSystem/Particle::set_angularVelocity3D(UnityEngine.Vector3) */,
	{ 0, 0 } /* 0x06000043 System.Single UnityEngine.ParticleSystem/Particle::GetCurrentSize(UnityEngine.ParticleSystem) */,
	{ 0, 0 } /* 0x06000044 UnityEngine.Color32 UnityEngine.ParticleSystem/Particle::GetCurrentColor(UnityEngine.ParticleSystem) */,
	{ 0, 0 } /* 0x06000045 System.Void UnityEngine.ParticleSystem/Particle::set_lifetime(System.Single) */,
	{ 0, 0 } /* 0x06000046 System.Int32 UnityEngine.ParticleSystemRenderer::GetMeshes(UnityEngine.Mesh[]) */,
};
#else
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[1] = { { 0, 0} };
#endif
#if IL2CPP_MONO_DEBUGGER
IL2CPP_EXTERN_C Il2CppSequencePoint g_sequencePointsUnityEngine_ParticleSystemModule[];
Il2CppSequencePoint g_sequencePointsUnityEngine_ParticleSystemModule[193] = 
{
	{ 63018, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 0 } /* seqPointIndex: 0 */,
	{ 63018, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 1 } /* seqPointIndex: 1 */,
	{ 63018, 1, 106, 106, 71, 72, 0, kSequencePointKind_Normal, 0, 2 } /* seqPointIndex: 2 */,
	{ 63018, 1, 106, 106, 73, 113, 1, kSequencePointKind_Normal, 0, 3 } /* seqPointIndex: 3 */,
	{ 63018, 1, 106, 106, 73, 113, 5, kSequencePointKind_StepOut, 0, 4 } /* seqPointIndex: 4 */,
	{ 63018, 1, 106, 106, 114, 115, 13, kSequencePointKind_Normal, 0, 5 } /* seqPointIndex: 5 */,
	{ 63019, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 6 } /* seqPointIndex: 6 */,
	{ 63019, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 7 } /* seqPointIndex: 7 */,
	{ 63019, 1, 107, 107, 61, 62, 0, kSequencePointKind_Normal, 0, 8 } /* seqPointIndex: 8 */,
	{ 63019, 1, 107, 107, 63, 98, 1, kSequencePointKind_Normal, 0, 9 } /* seqPointIndex: 9 */,
	{ 63019, 1, 107, 107, 63, 98, 4, kSequencePointKind_StepOut, 0, 10 } /* seqPointIndex: 10 */,
	{ 63019, 1, 107, 107, 99, 100, 12, kSequencePointKind_Normal, 0, 11 } /* seqPointIndex: 11 */,
	{ 63022, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 12 } /* seqPointIndex: 12 */,
	{ 63022, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 13 } /* seqPointIndex: 13 */,
	{ 63022, 1, 155, 155, 28, 29, 0, kSequencePointKind_Normal, 0, 14 } /* seqPointIndex: 14 */,
	{ 63022, 1, 155, 155, 30, 41, 1, kSequencePointKind_Normal, 0, 15 } /* seqPointIndex: 15 */,
	{ 63022, 1, 155, 155, 30, 41, 3, kSequencePointKind_StepOut, 0, 16 } /* seqPointIndex: 16 */,
	{ 63022, 1, 155, 155, 43, 44, 9, kSequencePointKind_Normal, 0, 17 } /* seqPointIndex: 17 */,
	{ 63024, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 18 } /* seqPointIndex: 18 */,
	{ 63024, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 19 } /* seqPointIndex: 19 */,
	{ 63024, 1, 176, 176, 37, 38, 0, kSequencePointKind_Normal, 0, 20 } /* seqPointIndex: 20 */,
	{ 63024, 1, 176, 176, 39, 60, 1, kSequencePointKind_Normal, 0, 21 } /* seqPointIndex: 21 */,
	{ 63024, 1, 176, 176, 39, 60, 3, kSequencePointKind_StepOut, 0, 22 } /* seqPointIndex: 22 */,
	{ 63024, 1, 176, 176, 61, 62, 9, kSequencePointKind_Normal, 0, 23 } /* seqPointIndex: 23 */,
	{ 63028, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 24 } /* seqPointIndex: 24 */,
	{ 63028, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 25 } /* seqPointIndex: 25 */,
	{ 63028, 2, 635, 635, 38, 39, 0, kSequencePointKind_Normal, 0, 26 } /* seqPointIndex: 26 */,
	{ 63028, 2, 635, 635, 40, 68, 1, kSequencePointKind_Normal, 0, 27 } /* seqPointIndex: 27 */,
	{ 63028, 2, 635, 635, 40, 68, 2, kSequencePointKind_StepOut, 0, 28 } /* seqPointIndex: 28 */,
	{ 63028, 2, 635, 635, 69, 70, 10, kSequencePointKind_Normal, 0, 29 } /* seqPointIndex: 29 */,
	{ 63029, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 30 } /* seqPointIndex: 30 */,
	{ 63029, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 31 } /* seqPointIndex: 31 */,
	{ 63029, 2, 654, 654, 72, 73, 0, kSequencePointKind_Normal, 0, 32 } /* seqPointIndex: 32 */,
	{ 63029, 2, 654, 654, 74, 119, 1, kSequencePointKind_Normal, 0, 33 } /* seqPointIndex: 33 */,
	{ 63029, 2, 654, 654, 74, 119, 2, kSequencePointKind_StepOut, 0, 34 } /* seqPointIndex: 34 */,
	{ 63029, 2, 654, 654, 120, 121, 10, kSequencePointKind_Normal, 0, 35 } /* seqPointIndex: 35 */,
	{ 63030, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 36 } /* seqPointIndex: 36 */,
	{ 63030, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 37 } /* seqPointIndex: 37 */,
	{ 63030, 3, 120, 120, 9, 10, 0, kSequencePointKind_Normal, 0, 38 } /* seqPointIndex: 38 */,
	{ 63030, 3, 121, 121, 13, 78, 1, kSequencePointKind_Normal, 0, 39 } /* seqPointIndex: 39 */,
	{ 63030, 3, 122, 122, 13, 42, 9, kSequencePointKind_Normal, 0, 40 } /* seqPointIndex: 40 */,
	{ 63030, 3, 122, 122, 13, 42, 12, kSequencePointKind_StepOut, 0, 41 } /* seqPointIndex: 41 */,
	{ 63030, 3, 123, 123, 13, 42, 18, kSequencePointKind_Normal, 0, 42 } /* seqPointIndex: 42 */,
	{ 63030, 3, 123, 123, 13, 42, 21, kSequencePointKind_StepOut, 0, 43 } /* seqPointIndex: 43 */,
	{ 63030, 3, 124, 124, 13, 42, 27, kSequencePointKind_Normal, 0, 44 } /* seqPointIndex: 44 */,
	{ 63030, 3, 124, 124, 13, 42, 31, kSequencePointKind_StepOut, 0, 45 } /* seqPointIndex: 45 */,
	{ 63030, 3, 125, 125, 13, 47, 37, kSequencePointKind_Normal, 0, 46 } /* seqPointIndex: 46 */,
	{ 63030, 3, 125, 125, 13, 47, 41, kSequencePointKind_StepOut, 0, 47 } /* seqPointIndex: 47 */,
	{ 63030, 3, 126, 126, 13, 39, 47, kSequencePointKind_Normal, 0, 48 } /* seqPointIndex: 48 */,
	{ 63030, 3, 126, 126, 13, 39, 50, kSequencePointKind_StepOut, 0, 49 } /* seqPointIndex: 49 */,
	{ 63030, 3, 127, 127, 13, 48, 56, kSequencePointKind_Normal, 0, 50 } /* seqPointIndex: 50 */,
	{ 63030, 3, 127, 127, 13, 48, 58, kSequencePointKind_StepOut, 0, 51 } /* seqPointIndex: 51 */,
	{ 63030, 3, 127, 127, 13, 48, 63, kSequencePointKind_StepOut, 0, 52 } /* seqPointIndex: 52 */,
	{ 63030, 3, 128, 128, 13, 55, 69, kSequencePointKind_Normal, 0, 53 } /* seqPointIndex: 53 */,
	{ 63030, 3, 128, 128, 13, 55, 71, kSequencePointKind_StepOut, 0, 54 } /* seqPointIndex: 54 */,
	{ 63030, 3, 128, 128, 13, 55, 76, kSequencePointKind_StepOut, 0, 55 } /* seqPointIndex: 55 */,
	{ 63030, 3, 129, 129, 13, 41, 82, kSequencePointKind_Normal, 0, 56 } /* seqPointIndex: 56 */,
	{ 63030, 3, 129, 129, 13, 41, 86, kSequencePointKind_StepOut, 0, 57 } /* seqPointIndex: 57 */,
	{ 63030, 3, 130, 130, 13, 37, 92, kSequencePointKind_Normal, 0, 58 } /* seqPointIndex: 58 */,
	{ 63030, 3, 130, 130, 13, 37, 95, kSequencePointKind_StepOut, 0, 59 } /* seqPointIndex: 59 */,
	{ 63030, 3, 131, 131, 13, 44, 101, kSequencePointKind_Normal, 0, 60 } /* seqPointIndex: 60 */,
	{ 63030, 3, 131, 131, 13, 44, 104, kSequencePointKind_StepOut, 0, 61 } /* seqPointIndex: 61 */,
	{ 63030, 3, 132, 132, 9, 10, 110, kSequencePointKind_Normal, 0, 62 } /* seqPointIndex: 62 */,
	{ 63031, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 63 } /* seqPointIndex: 63 */,
	{ 63031, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 64 } /* seqPointIndex: 64 */,
	{ 63031, 3, 136, 136, 9, 10, 0, kSequencePointKind_Normal, 0, 65 } /* seqPointIndex: 65 */,
	{ 63031, 3, 137, 137, 13, 44, 1, kSequencePointKind_Normal, 0, 66 } /* seqPointIndex: 66 */,
	{ 63031, 3, 137, 137, 13, 44, 4, kSequencePointKind_StepOut, 0, 67 } /* seqPointIndex: 67 */,
	{ 63031, 3, 138, 138, 9, 10, 10, kSequencePointKind_Normal, 0, 68 } /* seqPointIndex: 68 */,
	{ 63034, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 69 } /* seqPointIndex: 69 */,
	{ 63034, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 70 } /* seqPointIndex: 70 */,
	{ 63034, 2, 20, 20, 64, 65, 0, kSequencePointKind_Normal, 0, 71 } /* seqPointIndex: 71 */,
	{ 63034, 2, 20, 20, 66, 100, 1, kSequencePointKind_Normal, 0, 72 } /* seqPointIndex: 72 */,
	{ 63034, 2, 20, 20, 101, 102, 8, kSequencePointKind_Normal, 0, 73 } /* seqPointIndex: 73 */,
	{ 63047, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 74 } /* seqPointIndex: 74 */,
	{ 63047, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 75 } /* seqPointIndex: 75 */,
	{ 63047, 2, 531, 531, 81, 82, 0, kSequencePointKind_Normal, 0, 76 } /* seqPointIndex: 76 */,
	{ 63047, 2, 531, 531, 83, 117, 1, kSequencePointKind_Normal, 0, 77 } /* seqPointIndex: 77 */,
	{ 63047, 2, 531, 531, 118, 119, 8, kSequencePointKind_Normal, 0, 78 } /* seqPointIndex: 78 */,
	{ 63062, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 79 } /* seqPointIndex: 79 */,
	{ 63062, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 80 } /* seqPointIndex: 80 */,
	{ 63062, 4, 88, 88, 50, 51, 0, kSequencePointKind_Normal, 0, 81 } /* seqPointIndex: 81 */,
	{ 63062, 4, 88, 88, 52, 70, 1, kSequencePointKind_Normal, 0, 82 } /* seqPointIndex: 82 */,
	{ 63062, 4, 88, 88, 71, 72, 10, kSequencePointKind_Normal, 0, 83 } /* seqPointIndex: 83 */,
	{ 63063, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 84 } /* seqPointIndex: 84 */,
	{ 63063, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 85 } /* seqPointIndex: 85 */,
	{ 63063, 4, 91, 91, 41, 42, 0, kSequencePointKind_Normal, 0, 86 } /* seqPointIndex: 86 */,
	{ 63063, 4, 91, 91, 43, 64, 1, kSequencePointKind_Normal, 0, 87 } /* seqPointIndex: 87 */,
	{ 63063, 4, 91, 91, 65, 66, 10, kSequencePointKind_Normal, 0, 88 } /* seqPointIndex: 88 */,
	{ 63064, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 89 } /* seqPointIndex: 89 */,
	{ 63064, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 90 } /* seqPointIndex: 90 */,
	{ 63064, 4, 92, 92, 47, 48, 0, kSequencePointKind_Normal, 0, 91 } /* seqPointIndex: 91 */,
	{ 63064, 4, 92, 92, 49, 67, 1, kSequencePointKind_Normal, 0, 92 } /* seqPointIndex: 92 */,
	{ 63064, 4, 92, 92, 68, 69, 10, kSequencePointKind_Normal, 0, 93 } /* seqPointIndex: 93 */,
	{ 63065, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 94 } /* seqPointIndex: 94 */,
	{ 63065, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 95 } /* seqPointIndex: 95 */,
	{ 63065, 4, 190, 190, 43, 44, 0, kSequencePointKind_Normal, 0, 96 } /* seqPointIndex: 96 */,
	{ 63065, 4, 190, 190, 45, 63, 1, kSequencePointKind_Normal, 0, 97 } /* seqPointIndex: 97 */,
	{ 63065, 4, 190, 190, 64, 65, 10, kSequencePointKind_Normal, 0, 98 } /* seqPointIndex: 98 */,
	{ 63066, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 99 } /* seqPointIndex: 99 */,
	{ 63066, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 100 } /* seqPointIndex: 100 */,
	{ 63066, 4, 190, 190, 70, 71, 0, kSequencePointKind_Normal, 0, 101 } /* seqPointIndex: 101 */,
	{ 63066, 4, 190, 190, 72, 91, 1, kSequencePointKind_Normal, 0, 102 } /* seqPointIndex: 102 */,
	{ 63066, 4, 190, 190, 92, 93, 8, kSequencePointKind_Normal, 0, 103 } /* seqPointIndex: 103 */,
	{ 63067, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 104 } /* seqPointIndex: 104 */,
	{ 63067, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 105 } /* seqPointIndex: 105 */,
	{ 63067, 4, 191, 191, 70, 71, 0, kSequencePointKind_Normal, 0, 106 } /* seqPointIndex: 106 */,
	{ 63067, 4, 191, 191, 72, 91, 1, kSequencePointKind_Normal, 0, 107 } /* seqPointIndex: 107 */,
	{ 63067, 4, 191, 191, 92, 93, 8, kSequencePointKind_Normal, 0, 108 } /* seqPointIndex: 108 */,
	{ 63068, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 109 } /* seqPointIndex: 109 */,
	{ 63068, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 110 } /* seqPointIndex: 110 */,
	{ 63068, 4, 194, 194, 50, 51, 0, kSequencePointKind_Normal, 0, 111 } /* seqPointIndex: 111 */,
	{ 63068, 4, 194, 194, 52, 70, 1, kSequencePointKind_Normal, 0, 112 } /* seqPointIndex: 112 */,
	{ 63068, 4, 194, 194, 71, 72, 10, kSequencePointKind_Normal, 0, 113 } /* seqPointIndex: 113 */,
	{ 63069, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 114 } /* seqPointIndex: 114 */,
	{ 63069, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 115 } /* seqPointIndex: 115 */,
	{ 63069, 4, 194, 194, 77, 78, 0, kSequencePointKind_Normal, 0, 116 } /* seqPointIndex: 116 */,
	{ 63069, 4, 194, 194, 79, 98, 1, kSequencePointKind_Normal, 0, 117 } /* seqPointIndex: 117 */,
	{ 63069, 4, 194, 194, 99, 100, 8, kSequencePointKind_Normal, 0, 118 } /* seqPointIndex: 118 */,
	{ 63070, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 119 } /* seqPointIndex: 119 */,
	{ 63070, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 120 } /* seqPointIndex: 120 */,
	{ 63070, 4, 195, 195, 46, 47, 0, kSequencePointKind_Normal, 0, 121 } /* seqPointIndex: 121 */,
	{ 63070, 4, 195, 195, 48, 71, 1, kSequencePointKind_Normal, 0, 122 } /* seqPointIndex: 122 */,
	{ 63070, 4, 195, 195, 72, 73, 10, kSequencePointKind_Normal, 0, 123 } /* seqPointIndex: 123 */,
	{ 63071, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 124 } /* seqPointIndex: 124 */,
	{ 63071, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 125 } /* seqPointIndex: 125 */,
	{ 63071, 4, 195, 195, 78, 79, 0, kSequencePointKind_Normal, 0, 126 } /* seqPointIndex: 126 */,
	{ 63071, 4, 195, 195, 80, 104, 1, kSequencePointKind_Normal, 0, 127 } /* seqPointIndex: 127 */,
	{ 63071, 4, 195, 195, 105, 106, 8, kSequencePointKind_Normal, 0, 128 } /* seqPointIndex: 128 */,
	{ 63072, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 129 } /* seqPointIndex: 129 */,
	{ 63072, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 130 } /* seqPointIndex: 130 */,
	{ 63072, 4, 196, 196, 74, 75, 0, kSequencePointKind_Normal, 0, 131 } /* seqPointIndex: 131 */,
	{ 63072, 4, 196, 196, 76, 97, 1, kSequencePointKind_Normal, 0, 132 } /* seqPointIndex: 132 */,
	{ 63072, 4, 196, 196, 98, 99, 8, kSequencePointKind_Normal, 0, 133 } /* seqPointIndex: 133 */,
	{ 63073, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 134 } /* seqPointIndex: 134 */,
	{ 63073, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 135 } /* seqPointIndex: 135 */,
	{ 63073, 4, 197, 197, 44, 45, 0, kSequencePointKind_Normal, 0, 136 } /* seqPointIndex: 136 */,
	{ 63073, 4, 197, 197, 46, 66, 1, kSequencePointKind_Normal, 0, 137 } /* seqPointIndex: 137 */,
	{ 63073, 4, 197, 197, 67, 68, 10, kSequencePointKind_Normal, 0, 138 } /* seqPointIndex: 138 */,
	{ 63074, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 139 } /* seqPointIndex: 139 */,
	{ 63074, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 140 } /* seqPointIndex: 140 */,
	{ 63074, 4, 197, 197, 73, 74, 0, kSequencePointKind_Normal, 0, 141 } /* seqPointIndex: 141 */,
	{ 63074, 4, 197, 197, 75, 96, 1, kSequencePointKind_Normal, 0, 142 } /* seqPointIndex: 142 */,
	{ 63074, 4, 197, 197, 97, 98, 8, kSequencePointKind_Normal, 0, 143 } /* seqPointIndex: 143 */,
	{ 63075, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 144 } /* seqPointIndex: 144 */,
	{ 63075, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 145 } /* seqPointIndex: 145 */,
	{ 63075, 4, 200, 200, 72, 73, 0, kSequencePointKind_Normal, 0, 146 } /* seqPointIndex: 146 */,
	{ 63075, 4, 200, 200, 74, 121, 1, kSequencePointKind_Normal, 0, 147 } /* seqPointIndex: 147 */,
	{ 63075, 4, 200, 200, 74, 121, 5, kSequencePointKind_StepOut, 0, 148 } /* seqPointIndex: 148 */,
	{ 63075, 4, 200, 200, 122, 123, 15, kSequencePointKind_Normal, 0, 149 } /* seqPointIndex: 149 */,
	{ 63076, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 150 } /* seqPointIndex: 150 */,
	{ 63076, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 151 } /* seqPointIndex: 151 */,
	{ 63076, 4, 203, 203, 41, 42, 0, kSequencePointKind_Normal, 0, 152 } /* seqPointIndex: 152 */,
	{ 63076, 4, 203, 203, 43, 79, 1, kSequencePointKind_Normal, 0, 153 } /* seqPointIndex: 153 */,
	{ 63076, 4, 203, 203, 80, 81, 21, kSequencePointKind_Normal, 0, 154 } /* seqPointIndex: 154 */,
	{ 63077, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 155 } /* seqPointIndex: 155 */,
	{ 63077, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 156 } /* seqPointIndex: 156 */,
	{ 63077, 4, 204, 204, 45, 46, 0, kSequencePointKind_Normal, 0, 157 } /* seqPointIndex: 157 */,
	{ 63077, 4, 204, 204, 47, 81, 1, kSequencePointKind_Normal, 0, 158 } /* seqPointIndex: 158 */,
	{ 63077, 4, 204, 204, 47, 81, 12, kSequencePointKind_StepOut, 0, 159 } /* seqPointIndex: 159 */,
	{ 63077, 4, 204, 204, 82, 83, 20, kSequencePointKind_Normal, 0, 160 } /* seqPointIndex: 160 */,
	{ 63078, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 161 } /* seqPointIndex: 161 */,
	{ 63078, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 162 } /* seqPointIndex: 162 */,
	{ 63078, 4, 204, 204, 88, 89, 0, kSequencePointKind_Normal, 0, 163 } /* seqPointIndex: 163 */,
	{ 63078, 4, 204, 204, 90, 125, 1, kSequencePointKind_Normal, 0, 164 } /* seqPointIndex: 164 */,
	{ 63078, 4, 204, 204, 90, 125, 8, kSequencePointKind_StepOut, 0, 165 } /* seqPointIndex: 165 */,
	{ 63078, 4, 204, 204, 126, 162, 18, kSequencePointKind_Normal, 0, 166 } /* seqPointIndex: 166 */,
	{ 63078, 4, 204, 204, 163, 164, 32, kSequencePointKind_Normal, 0, 167 } /* seqPointIndex: 167 */,
	{ 63079, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 168 } /* seqPointIndex: 168 */,
	{ 63079, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 169 } /* seqPointIndex: 169 */,
	{ 63079, 4, 207, 207, 102, 103, 0, kSequencePointKind_Normal, 0, 170 } /* seqPointIndex: 170 */,
	{ 63079, 4, 207, 207, 104, 146, 1, kSequencePointKind_Normal, 0, 171 } /* seqPointIndex: 171 */,
	{ 63079, 4, 207, 207, 104, 146, 8, kSequencePointKind_StepOut, 0, 172 } /* seqPointIndex: 172 */,
	{ 63079, 4, 207, 207, 147, 183, 18, kSequencePointKind_Normal, 0, 173 } /* seqPointIndex: 173 */,
	{ 63079, 4, 207, 207, 184, 185, 32, kSequencePointKind_Normal, 0, 174 } /* seqPointIndex: 174 */,
	{ 63080, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 175 } /* seqPointIndex: 175 */,
	{ 63080, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 176 } /* seqPointIndex: 176 */,
	{ 63080, 4, 209, 209, 64, 65, 0, kSequencePointKind_Normal, 0, 177 } /* seqPointIndex: 177 */,
	{ 63080, 4, 209, 209, 66, 113, 1, kSequencePointKind_Normal, 0, 178 } /* seqPointIndex: 178 */,
	{ 63080, 4, 209, 209, 66, 113, 3, kSequencePointKind_StepOut, 0, 179 } /* seqPointIndex: 179 */,
	{ 63080, 4, 209, 209, 114, 115, 11, kSequencePointKind_Normal, 0, 180 } /* seqPointIndex: 180 */,
	{ 63081, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 181 } /* seqPointIndex: 181 */,
	{ 63081, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 182 } /* seqPointIndex: 182 */,
	{ 63081, 4, 211, 211, 67, 68, 0, kSequencePointKind_Normal, 0, 183 } /* seqPointIndex: 183 */,
	{ 63081, 4, 211, 211, 69, 117, 1, kSequencePointKind_Normal, 0, 184 } /* seqPointIndex: 184 */,
	{ 63081, 4, 211, 211, 69, 117, 3, kSequencePointKind_StepOut, 0, 185 } /* seqPointIndex: 185 */,
	{ 63081, 4, 211, 211, 118, 119, 11, kSequencePointKind_Normal, 0, 186 } /* seqPointIndex: 186 */,
	{ 63082, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 187 } /* seqPointIndex: 187 */,
	{ 63082, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 188 } /* seqPointIndex: 188 */,
	{ 63082, 3, 94, 94, 75, 76, 0, kSequencePointKind_Normal, 0, 189 } /* seqPointIndex: 189 */,
	{ 63082, 3, 94, 94, 77, 103, 1, kSequencePointKind_Normal, 0, 190 } /* seqPointIndex: 190 */,
	{ 63082, 3, 94, 94, 77, 103, 3, kSequencePointKind_StepOut, 0, 191 } /* seqPointIndex: 191 */,
	{ 63082, 3, 94, 94, 104, 105, 9, kSequencePointKind_Normal, 0, 192 } /* seqPointIndex: 192 */,
};
#else
extern Il2CppSequencePoint g_sequencePointsUnityEngine_ParticleSystemModule[];
Il2CppSequencePoint g_sequencePointsUnityEngine_ParticleSystemModule[1] = { { 0, 0, 0, 0, 0, 0, 0, kSequencePointKind_Normal, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#else
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[] = {
{ "", { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }, //0 
{ "\\home\\bokken\\buildslave\\unity\\build\\Modules\\ParticleSystem\\ScriptBindings\\ParticleSystem.bindings.cs", { 236, 65, 203, 223, 251, 100, 230, 5, 113, 251, 2, 165, 182, 52, 78, 2} }, //1 
{ "\\home\\bokken\\buildslave\\unity\\build\\Modules\\ParticleSystem\\ScriptBindings\\ParticleSystemModules.bindings.cs", { 3, 133, 160, 165, 97, 183, 14, 149, 224, 6, 3, 184, 42, 99, 31, 70} }, //2 
{ "\\home\\bokken\\buildslave\\unity\\build\\Modules\\ParticleSystem\\Managed\\ParticleSystem.deprecated.cs", { 254, 108, 111, 128, 100, 7, 222, 4, 156, 244, 245, 164, 175, 217, 189, 197} }, //3 
{ "\\home\\bokken\\buildslave\\unity\\build\\Modules\\ParticleSystem\\Managed\\ParticleSystemStructs.cs", { 220, 239, 182, 144, 220, 43, 200, 140, 248, 250, 235, 227, 210, 7, 223, 37} }, //4 
};
#else
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[1] = { NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppTypeSourceFilePair g_typeSourceFiles[8] = 
{
	{ 9236, 1 },
	{ 9236, 2 },
	{ 9236, 3 },
	{ 9231, 2 },
	{ 9232, 2 },
	{ 9233, 4 },
	{ 9234, 4 },
	{ 9234, 3 },
};
#else
static const Il2CppTypeSourceFilePair g_typeSourceFiles[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodScope g_methodScopes[16] = 
{
	{ 0, 15 },
	{ 0, 14 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 111 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 12 },
	{ 0, 23 },
	{ 0, 22 },
	{ 0, 13 },
	{ 0, 13 },
};
#else
static const Il2CppMethodScope g_methodScopes[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[70] = 
{
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::set_time(System.Single) */,
	{ 0, 0, 0 } /* System.Single UnityEngine.ParticleSystem::GetParticleCurrentSize(UnityEngine.ParticleSystem/Particle&) */,
	{ 0, 0, 0 } /* UnityEngine.Color32 UnityEngine.ParticleSystem::GetParticleCurrentColor(UnityEngine.ParticleSystem/Particle&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[],System.Int32,System.Int32) */,
	{ 15, 0, 1 } /* System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[],System.Int32) */,
	{ 14, 1, 1 } /* System.Int32 UnityEngine.ParticleSystem::GetParticles(UnityEngine.ParticleSystem/Particle[]) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Simulate(System.Single,System.Boolean,System.Boolean,System.Boolean) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Play(System.Boolean) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Play() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Stop(System.Boolean,UnityEngine.ParticleSystemStopBehavior) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Emit(System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Emit_Internal(System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.ParticleSystem/EmitParams,System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::EmitOld_Internal(UnityEngine.ParticleSystem/Particle&) */,
	{ 12, 2, 1 } /* UnityEngine.ParticleSystem/MainModule UnityEngine.ParticleSystem::get_main() */,
	{ 12, 3, 1 } /* UnityEngine.ParticleSystem/TextureSheetAnimationModule UnityEngine.ParticleSystem::get_textureSheetAnimation() */,
	{ 111, 4, 1 } /* System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.Vector3,UnityEngine.Vector3,System.Single,System.Single,UnityEngine.Color32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Emit(UnityEngine.ParticleSystem/Particle) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::GetParticleCurrentColor_Injected(UnityEngine.ParticleSystem/Particle&,UnityEngine.Color32&) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem::Emit_Injected(UnityEngine.ParticleSystem/EmitParams&,System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/MainModule::.ctor(UnityEngine.ParticleSystem) */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemSimulationSpace UnityEngine.ParticleSystem/MainModule::get_simulationSpace() */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemScalingMode UnityEngine.ParticleSystem/MainModule::get_scalingMode() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/MainModule::set_scalingMode(UnityEngine.ParticleSystemScalingMode) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.ParticleSystem/MainModule::get_playOnAwake() */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/MainModule::get_maxParticles() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/MainModule::set_maxParticles(System.Int32) */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemSimulationSpace UnityEngine.ParticleSystem/MainModule::get_simulationSpace_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemScalingMode UnityEngine.ParticleSystem/MainModule::get_scalingMode_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/MainModule::set_scalingMode_Injected(UnityEngine.ParticleSystem/MainModule&,UnityEngine.ParticleSystemScalingMode) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.ParticleSystem/MainModule::get_playOnAwake_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/MainModule::get_maxParticles_Injected(UnityEngine.ParticleSystem/MainModule&) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/MainModule::set_maxParticles_Injected(UnityEngine.ParticleSystem/MainModule&,System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/TextureSheetAnimationModule::.ctor(UnityEngine.ParticleSystem) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_enabled() */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesX() */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesY() */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemAnimationType UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_animation() */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystem/MinMaxCurve UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_frameOverTime() */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_cycleCount() */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_rowIndex() */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_enabled_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesX_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_numTilesY_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0, 0 } /* UnityEngine.ParticleSystemAnimationType UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_animation_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_frameOverTime_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&,UnityEngine.ParticleSystem/MinMaxCurve&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_cycleCount_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystem/TextureSheetAnimationModule::get_rowIndex_Injected(UnityEngine.ParticleSystem/TextureSheetAnimationModule&) */,
	{ 12, 5, 1 } /* UnityEngine.AnimationCurve UnityEngine.ParticleSystem/MinMaxCurve::get_curveMin() */,
	{ 12, 6, 1 } /* System.Single UnityEngine.ParticleSystem/MinMaxCurve::get_constant() */,
	{ 12, 7, 1 } /* UnityEngine.AnimationCurve UnityEngine.ParticleSystem/MinMaxCurve::get_curve() */,
	{ 12, 8, 1 } /* UnityEngine.Vector3 UnityEngine.ParticleSystem/Particle::get_position() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_position(UnityEngine.Vector3) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_velocity(UnityEngine.Vector3) */,
	{ 12, 9, 1 } /* System.Single UnityEngine.ParticleSystem/Particle::get_remainingLifetime() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_remainingLifetime(System.Single) */,
	{ 12, 10, 1 } /* System.Single UnityEngine.ParticleSystem/Particle::get_startLifetime() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_startLifetime(System.Single) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_startColor(UnityEngine.Color32) */,
	{ 12, 11, 1 } /* System.UInt32 UnityEngine.ParticleSystem/Particle::get_randomSeed() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_randomSeed(System.UInt32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_startSize(System.Single) */,
	{ 23, 12, 1 } /* System.Single UnityEngine.ParticleSystem/Particle::get_rotation() */,
	{ 22, 13, 1 } /* UnityEngine.Vector3 UnityEngine.ParticleSystem/Particle::get_rotation3D() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_rotation3D(UnityEngine.Vector3) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_angularVelocity3D(UnityEngine.Vector3) */,
	{ 13, 14, 1 } /* System.Single UnityEngine.ParticleSystem/Particle::GetCurrentSize(UnityEngine.ParticleSystem) */,
	{ 13, 15, 1 } /* UnityEngine.Color32 UnityEngine.ParticleSystem/Particle::GetCurrentColor(UnityEngine.ParticleSystem) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.ParticleSystem/Particle::set_lifetime(System.Single) */,
	{ 0, 0, 0 } /* System.Int32 UnityEngine.ParticleSystemRenderer::GetMeshes(UnityEngine.Mesh[]) */,
};
#else
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[1] = { { 0, 0, 0 } };
#endif
IL2CPP_EXTERN_C const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationUnityEngine_ParticleSystemModule;
const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationUnityEngine_ParticleSystemModule = 
{
	(Il2CppMethodExecutionContextInfo*)g_methodExecutionContextInfos,
	(Il2CppMethodExecutionContextInfoIndex*)g_methodExecutionContextInfoIndexes,
	(Il2CppMethodScope*)g_methodScopes,
	(Il2CppMethodHeaderInfo*)g_methodHeaderInfos,
	(Il2CppSequencePointSourceFile*)g_sequencePointSourceFiles,
	193,
	(Il2CppSequencePoint*)g_sequencePointsUnityEngine_ParticleSystemModule,
	0,
	(Il2CppCatchPoint*)g_catchPoints,
	8,
	(Il2CppTypeSourceFilePair*)g_typeSourceFiles,
	(const char**)g_methodExecutionContextInfoStrings,
};