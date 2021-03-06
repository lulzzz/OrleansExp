using Engine.Controller;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexeraUtilities;
using Engine.OperatorImplementation.SendingSemantics;
using Orleans.Core;
using Orleans.Runtime;
using Engine.DeploySemantics;
using Engine.Breakpoint.LocalBreakpoint;
using Engine.Breakpoint.GlobalBreakpoint;

namespace Engine.OperatorImplementation.Common
{
    //Administrator actor of an operator in the workflow.
    //It administrates worker actors of its operator
    public interface IPrincipalGrain : IGrainWithGuidKey
    {
        Task Start();
        Task Pause();
        Task Resume();
        Task Deactivate();
        Task StashOutput();
        Task ReleaseOutput();
        Task Init(IControllerGrain controllerGrain, Operator op, List<Pair<Operator,WorkerLayer>> prev);
        Task<WorkerLayer> GetInputLayer();
        Task<WorkerLayer> GetOutputLayer();
        Task SetBreakpoint(GlobalBreakpointBase breakpoint);
        Task OnWorkerLocalBreakpointTriggered(IWorkerGrain sender, List<LocalBreakpointBase> breakpoint);
        Task OnWorkerDidPaused(IWorkerGrain sender);
        Task OnWorkerFinished(IWorkerGrain sender);
        Task OnWorkerReceivedAllBatches(IWorkerGrain sender);
    }
}