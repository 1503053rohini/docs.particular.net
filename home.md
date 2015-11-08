<style>
  .small.button{
	line-height: 45px;
	font-size: 16px;
	padding-left: 15px;
	font-family: 'Lato',Bold;
	display: inline-block;
  }
  .small.button a{
	display: block;
	color: white;		
	line-height: 45px;
	width: 215px;
	background-color: #00a3c4;
	border-bottom: 5px solid #0071a0;
	clear: both;
	text-align: center;
	text-transform: uppercase;
	text-decoration: none;
	font-weight: 700;	
  }
  .small.button a:hover{
	background-color: #03AFF8;
  }
  .block.black a, .block.middle a, .productcolumn a {
    color: inherit;
	white-space: nowrap;
  }
  .block{
    width: 100%;
    background-color: rgb(233,240,242);
    padding: 21px;
    margin-bottom: 2px;
    font-family: 'Lato';
  }

  .block.top img, .productcolumn img {
    float: left;
  }
  .block.black img{
    float: inherit;
  }
  .block.top .button{
    float: right;
    width: 225px;
    font-size: 15px;
  }
  .block.middle .ic{
    min-width: 25%;
    float: left;
    text-align: center;
    font-size: 18px;
    font-weight: bold;
    line-height: 50px;
    color: rgb(0,114,156);
  }      
  .block.black{
    margin-top: -2px;
    margin-bottom: 0px;
    width: 100%;
    clear: both;
    background-color: rgb(26,26,26);
    font-size: 16px;
    font-weight: bold;
    padding-top: 13px;
    padding-bottom: 13px;
    line-height: 30px;
  }
  span.blue{
    color: rgb(0,163,196);
    padding-right: 30px;
    display: inline-block;
  }
  .block.black span img{
    padding-left: 0px;
    padding-right: 5px;
    margin-top: -3px;
  }
  .productcolumn .black{
    font-size: 14px;
  }
  .block .left2 {
    width: 50%;
    float: left;
    padding-right: 20px;
    min-width: 360px;
  }
  .block .right1 {
    width: 50%;
    float: left;
    padding-left: 20px;
    border-left: 2px solid rgb(218,222,222);
  }
  .block h2{
    clear: both;
    font-size: 20px !important;
    font-family: 'Dosis', Semibold;
    padding-bottom: 20px;
    margin-bottom: 0px;
    margin-top: 0px;
  }
  .block h3{
    font-weight: bold;
    font-size: 17px;
    margin-top: 0px;
    margin-bottom: 0px;
    color: rgb(0,114,156);
  }
  .block h4{
    font-size: 16px !important;
    font-family: 'Dosis', bold;
    font-weight: bold;
    margin-top: 0px;
  }
  .block h5{        
    color: rgb(0,114,156);
    font-size: 14px;
    font-weight: bold;
    padding-left: 28px;
    padding-top: 5px;
  }
  .block p{
    font-size: 14px;
    color: rgb(77,77,77);
  }
  .block .right1 img, .block .left2 img {
    float: left;
    margin: 0px 13px 17px 0px;
  }
  .productcolumn{
    width: 32%;
    margin-right: 2%;
    float: left;        
  }
  .productcolumn.header{
    margin-top: 35px;        
  }
  .productcolumn.last{
    margin-right: 0px;
  }
  .productcolumnc{
    overflow: hidden;
    clear: both;
  }
  .productcolumnc .productcolumn{
    padding-bottom: 1000px;
    margin-bottom: -1000px;
  }
  
  .productcolumnc ul {
    list-style: none;
    margin-left: 0px;
    padding-left: 0px;
  }
  .productcolumnc li {
    color: rgb(0,114,156) !important;
    font-size: 14px;
    font-weight: bold;
    padding-bottom: 7px;
    padding-left: 12px;
    text-indent: -12px;
  }
  .productcolumnc li:before{
    content: "• ";
    color: rgb(0,114,156);
  }
</style>

<div class="block top">
  <a href="/nservicebus"><img src="/home/nservicebus.png" style="max-width: 43%" /></a>
  <div class="small button">
    <a class="blue" href="/nservicebus/">Documentation topics</a>
  </div>
  <div style="clear: both"></div>
</div>
<div class="block middle">
  <div class="ic">
    <a href="/samples/step-by-step/">
      <img src="/home/getting-started.png" /><br/>
      Getting Started
    </a>
  </div>
  <div class="ic">
    <a href="http://particular.net/Videos-and-Presentations">
      <img src="/home/intro-videos.png" /><br/>
      Intro Videos
    </a>
  </div>
  <div class="ic">
    <a href="https://groups.google.com/forum/#!forum/particularsoftware">
      <img src="/home/discussion-large.png" /><br/>
      Discussion group
    </a>
  </div>
  <div class="ic">
    <a href="/samples/" class="rarr">
      <img src="/home/samples.png" /><br/>
      Samples
    </a>
  </div>
  <div style="clear: both"></div>
</div>
<div class="block black">
  <span class="blue"><a href="https://github.com/Particular/NServiceBus/releases"><img src="/home/release-notes.png" /> Release notes</a></span><span class="blue"><a href="http://particular.net/downloads"><img src="/home/download.png" /> Downloads</a></span>
</div>
<div class="block middle">
<div class="left2">
    <a href="/platform/">
      <img src="/home/platform-small.png" />
      <h3>Particular Service Platform Overview</h3>
    </a>
    <p>A short and high level overview of the Platform</p>
    <div style="clear: both"></div>
    <a href="https://www.packtpub.com/application-development/learning-nservicebus-second-edition">
      <img src="/home/book.png" />
      <h3>Learning NServiceBus - Second Edition</h3>
    </a>
    <p>Book by David Boike on building reliable and scalable software with NServiceBus.</p>
    <div style="clear: both"></div>
    <a href="/platform/extensions.md">
      <img src="/home/extensions-small.png" />
      <h3>Extensions and integrations</h3>
    </a>
    <p>Extensions to NServiceBus developed by both the community and Particular</p>
    <div style="clear: both"></div>
  </div>
  <div class="right1">
    <a href="http://particular.net/HandsOnLabs">
      <img src="/home/hand-on-labs-small.png" />
      <h3>Hands-On Labs</h3>
    </a>
    <div style="clear: both"></div>
    <a href="http://stackoverflow.com/questions/tagged/nservicebus">
      <img src="/home/stackoverflow-big.png" />
      <h3>StackOverflow</h3>
    </a>
    <p>All questions and answers tagged 'NServiceBus'</p>
    <div style="clear: both"></div>
    <a href="http://www.pluralsight.com/courses/microservices-nservicebus-scaling-applications">
      <img src="/home/pluralsight.png">
      <h3>Pluralsight: Scaling Applications with<br> Microservices and NServiceBus</h3>
    </a>
    <div style="clear: both"></div>
    <a href="http://www.pluralsight.com/courses/nservicebus">
      <img src="/home/pluralsight.png">
      <h3>Pluralsight: Introduction to NServiceBus</h3>
    </a>
    <div style="clear: both"></div>
  </div>
  <div style="clear: both"></div>
</div>
<div class="productcolumn header">
  <div class="block top">
    <a href="/serviceinsight/">
      <img src="/home/serviceinsight.png" />
    </a>
    <div style="clear: both"></div>
  </div>
</div>
<div class="productcolumn header">
  <div class="block top">
    <a href="/servicecontrol/">
      <img src="/home/servicecontrol.png" />
    </a>
    <div style="clear: both"></div>
  </div>
</div>
<div class="productcolumn header last">
  <div class="block top">
    <a href="/servicepulse/">
      <img src="/home/servicepulse.png" />
    </a>
    <div style="clear: both"></div>
  </div>
</div>
<div class="productcolumnc">
  <div class="productcolumn block">
    <p><h4>Complete under-the-hood visualization of your system's behavior</h4></p>
    <ul>
      <li><a href="/serviceinsight/application-invocation.md">Application invocation</a></li>
      <li><a href="/serviceinsight/how-logging-works.md">Logging in ServiceInsight</a></li>
    </ul>
    <a href="/serviceinsight/"><h3>All ServiceInsight articles</h3></a><br/>
    <div style="clear: both"></div>
  </div>
  <div class="productcolumn block">
    <p><h4>Gather all the data and monitor multiple endpoints from a single location</h4></p>
    <ul>
      <li><a href="/servicecontrol/installation.md">Installing ServiceControl</a></li>
      <li><a href="/servicecontrol/multi-transport-support.md">Multi Transport Support</a></li>
      <li><a href="/servicecontrol/plugins/">Endpoint Plugins</a></li>
    </ul>
    <a href="/servicecontrol/"><h3>All ServiceControl articles</h3></a><br/>
   <div style="clear: both"></div>
  </div>
  <div class="productcolumn last block">
    <p><h4>Real-time monitoring that is custom tailored to fit your distributed systems</h4></p>
    <ul>
      <li><a href="/servicepulse/intro-endpoints-heartbeats.md">Monitoring Endpoints</a></li>
      <li><a href="/servicepulse/intro-failed-messages.md">Handling Failed Messages</a></li>
      <li><a href="/servicepulse/intro-endpoints-custom-checks.md">Introduction to Custom Checks</a></li>
    </ul>
    <a href="/servicepulse/"><h3>All ServicePulse articles</h3></a><br/>
    <div style="clear: both"></div>
  </div>
</div>
<div class="productcolumn">
  <div class="block black">
    <span class="blue"><a href="https://github.com/Particular/ServiceInsight/releases"><img src="/home/release-notes.png" />All Release notes</a></span>
  </div>
</div>
<div class="productcolumn">
  <div class="block black">
    <span class="blue"><a href="https://github.com/Particular/ServiceControl/releases"><img src="/home/release-notes.png" />Release notes</a></span>
  </div>
</div>
<div class="productcolumn last">
  <div class="block black">
    <span class="blue"><a href="https://github.com/Particular/ServicePulse/releases"><img src="/home/release-notes.png" />Release notes</a></span>
  </div>
</div>
<div style="clear: both; padding-top: 35px"></div>
