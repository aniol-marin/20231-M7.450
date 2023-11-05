using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.PresentationLayer
{
    public class PathsController : ControllerNode
    {
        [Injected] private readonly EntityMappingService mapping;
        [Injected] private readonly PostOffice post;
        private readonly List<Path> paths = new();

        public override void Init()
        {
            base.Init();

            // TO DO consider fetching in level baking
            ReportPaths();
        }

        public override void Deinit() { }

        public PathView GetRandomPath()
        {
            return paths
                .ElementAt(RandomValue.Int(0, paths.Count))
                .Convert(p => mapping.GetView(p.Id)) as PathView;
        }

        private void ReportPaths()
        {
            transform
                .GetComponentsInChildren<PathView>()
                .Join(v => new Path(v.InterestPoints))
                .SideEffect(t => mapping.Add(t.Item2, t.Item1.Origin))
                .SideEffect(t => paths.Add(t.Item2))
                .Select(t => t.Item2)
                .ForEach(post.FileNewEntity);
        }
    }
}