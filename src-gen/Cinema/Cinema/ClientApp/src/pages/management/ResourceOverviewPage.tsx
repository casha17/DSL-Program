import { Accordion, AccordionDetails, AccordionSummary, Button, Typography } from "@material-ui/core";
import { ExpandMore } from "@material-ui/icons";
import React from "react";
import { useHistory } from "react-router";

const ResourceOverviewPage = () => {

    const history = useHistory();

    const render = () => {

        return <div style={{display: "flex", width: "100%", justifyContent: "center", flexDirection: "column", padding: "20px"}}>
            <Typography style={{textAlign: "center", width: "100%"}} variant="h2">System Resources</Typography>
        
           <Accordion>
                       <AccordionSummary
                       expandIcon={<ExpandMore/>}
                       >
                           <Typography>Clients</Typography>
                       </AccordionSummary>
                       <AccordionDetails>
                           <div style={{display: "flex", flexDirection: "column"}}>
                               <Typography>
                                   Resource description goes here, manage Clients below:
                               </Typography>
                               <div style={{paddingTop: "20px", display: "flex"}}>
                                   <Button onClick={() => history.push("/management/Client_create")} variant="outlined" color="primary">Create Client</Button>
                                   <div style={{paddingRight: "10px"}}></div>
                                   <Button onClick={() => history.push("/management/Clients_overview")} variant="outlined" color="primary">Clients Overview</Button>
                               </div>
                           </div>
                       </AccordionDetails>
                   </Accordion>
           <Accordion>
                       <AccordionSummary
                       expandIcon={<ExpandMore/>}
                       >
                           <Typography>Admins</Typography>
                       </AccordionSummary>
                       <AccordionDetails>
                           <div style={{display: "flex", flexDirection: "column"}}>
                               <Typography>
                                   Resource description goes here, manage Admins below:
                               </Typography>
                               <div style={{paddingTop: "20px", display: "flex"}}>
                                   <Button onClick={() => history.push("/management/Admin_create")} variant="outlined" color="primary">Create Admin</Button>
                                   <div style={{paddingRight: "10px"}}></div>
                                   <Button onClick={() => history.push("/management/Admins_overview")} variant="outlined" color="primary">Admins Overview</Button>
                               </div>
                           </div>
                       </AccordionDetails>
                   </Accordion>
           <Accordion>
                       <AccordionSummary
                       expandIcon={<ExpandMore/>}
                       >
                           <Typography>CinemaHalls</Typography>
                       </AccordionSummary>
                       <AccordionDetails>
                           <div style={{display: "flex", flexDirection: "column"}}>
                               <Typography>
                                   Resource description goes here, manage CinemaHalls below:
                               </Typography>
                               <div style={{paddingTop: "20px", display: "flex"}}>
                                   <Button onClick={() => history.push("/management/CinemaHall_create")} variant="outlined" color="primary">Create CinemaHall</Button>
                                   <div style={{paddingRight: "10px"}}></div>
                                   <Button onClick={() => history.push("/management/CinemaHalls_overview")} variant="outlined" color="primary">CinemaHalls Overview</Button>
                               </div>
                           </div>
                       </AccordionDetails>
                   </Accordion>
           <Accordion>
                       <AccordionSummary
                       expandIcon={<ExpandMore/>}
                       >
                           <Typography>Seats</Typography>
                       </AccordionSummary>
                       <AccordionDetails>
                           <div style={{display: "flex", flexDirection: "column"}}>
                               <Typography>
                                   Resource description goes here, manage Seats below:
                               </Typography>
                               <div style={{paddingTop: "20px", display: "flex"}}>
                                   <Button onClick={() => history.push("/management/Seat_create")} variant="outlined" color="primary">Create Seat</Button>
                                   <div style={{paddingRight: "10px"}}></div>
                                   <Button onClick={() => history.push("/management/Seats_overview")} variant="outlined" color="primary">Seats Overview</Button>
                               </div>
                           </div>
                       </AccordionDetails>
                   </Accordion>
           <Accordion>
                       <AccordionSummary
                       expandIcon={<ExpandMore/>}
                       >
                           <Typography>NightPlans</Typography>
                       </AccordionSummary>
                       <AccordionDetails>
                           <div style={{display: "flex", flexDirection: "column"}}>
                               <Typography>
                                   Resource description goes here, manage NightPlans below:
                               </Typography>
                               <div style={{paddingTop: "20px", display: "flex"}}>
                                   <Button onClick={() => history.push("/management/NightPlan_create")} variant="outlined" color="primary">Create NightPlan</Button>
                                   <div style={{paddingRight: "10px"}}></div>
                                   <Button onClick={() => history.push("/management/NightPlans_overview")} variant="outlined" color="primary">NightPlans Overview</Button>
                               </div>
                           </div>
                       </AccordionDetails>
                   </Accordion>
        </div>
    }

    return render();
}

export default ResourceOverviewPage;
