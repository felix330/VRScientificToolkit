CreateOtherHeatmap <- function()
{
  est <- bkde2D(onlyxz2, bandwidth=c(0.1,0.1),gridsize=c(1000,1000),range.x=list(c(-3,3),c(-3,3)));
  
  est.raster = raster(list(x=est$x1,y=est$x2,z=est$fhat));
  projection(est.raster) <- CRS("+init=epsg:4326");
  xmin(est.raster) <- -5;
  xmax(est.raster) <- 5;
  ymin(est.raster) <- -5;
  ymax(est.raster) <- 5;
  plot(est.raster);
}