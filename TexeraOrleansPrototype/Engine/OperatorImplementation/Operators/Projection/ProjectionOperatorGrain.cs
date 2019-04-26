// #define PRINT_MESSAGE_ON
//#define PRINT_DROPPED_ON


using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Orleans.Concurrency;
using Engine.OperatorImplementation.MessagingSemantics;
using Engine.OperatorImplementation.Common;
using TexeraUtilities;

namespace Engine.OperatorImplementation.Operators
{
    public class ProjectionOperatorGrain : WorkerGrain, IProjectionOperatorGrain
    {
        List<int> projectionAttrs;
        public override Task Init(IWorkerGrain self, PredicateBase predicate, IPrincipalGrain principalGrain)
        {
            base.Init(self,predicate,principalGrain);
            projectionAttrs=((ProjectionPredicate)predicate).ProjectionAttrs;
            return Task.CompletedTask;
        }


        protected override List<TexeraTuple> ProcessTuple(TexeraTuple tuple)
        {
            TexeraTuple result=new TexeraTuple(tuple.TableID,new string[projectionAttrs.Count]);
            int i=0;
            foreach(int attr in projectionAttrs)
            {
                result.FieldList[i++]=tuple.FieldList[attr];
            }
            return new List<TexeraTuple>{result};
        }
    }
}