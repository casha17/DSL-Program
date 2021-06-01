import { Accordion, AccordionDetails, AccordionSummary, Button, CircularProgress, Collapse, FormControl, InputLabel, MenuItem, Select, Typography } from "@material-ui/core";
import { ExpandMore } from "@material-ui/icons";
import { Alert, AlertTitle } from "@material-ui/lab";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { httpGet, httpPost } from "../api/httpClient";
import { useMount } from "../lifeCycleExtensions";
import { CreateCinemaBookingRequestModel } from "../api/requestModels/CreateCinemaBookingRequestModel";
import { Seat } from "../api/models/resources/Seat";
import { NightPlan } from "../api/models/schedules/NightPlan";
import { Client } from "../api/models/customers/Client";
import { Admin } from "../api/models/customers/Admin";

const BookingPage = () => {
	
	const params = useParams() as { id: string, type: string };
	
	const [loadUser, setLoadUser] = useState(false);
    const [loadUserError, setLoadUserError] = useState<string>()
	const [userClient, setUserClient] = useState<Client>();
	const [userAdmin, setUserAdmin] = useState<Admin>();
	
	const [loadCinemaBookingResources, setLoadCinemaBookingResources] = useState(false);
		    const [loadErrorCinemaBookingResources, setLoadErrorCinemaBookingResources] = useState<string>();
		    const [openCinemaBookingResource, setOpenCinemaBookingResource] = useState(false);
		    const [CinemaBookingResource, setCinemaBookingResource] = useState<Seat[]>([]);
		    const [selectedCinemaBookingResource, setSelectedCinemaBookingResource] = useState<string>('');
		    const [loadCinemaBookingResourceSchedules, setLoadCinemaBookingResourceSchedules] = useState(false);
		    const [loadErrorCinemaBookingResourceSchedules, setLoadErrorCinemaBookingResourceSchedules] = useState<string>()
		    const [CinemaBookingResourceSchedules, setCinemaBookingResourceSchedules] = useState<NightPlan[]>([]);
		    const [selectedCinemaBookingResourceSchedule, setSelectedCinemaBookingResourceSchedule] = useState<string>('');
		    const [submittingCinemaBooking, setSubmittingCinemaBooking] = useState(false);
		    const [submittingCinemaBookingError, setSubmittingCinemaBookingError] = useState<string>();
	
	useMount(() => {
		if(params.type === "Client") {
		            fetchClient();
		        }
		if(params.type === "Admin") {
		            fetchAdmin();
		        }
    })
    
    const fetchClient = async () => {
	            setLoadUser(true);
	    
	            var result = await httpGet<Client>(`/Client/${params.id}`)
	            console.log(result)
	            if(result.isSuccess) {
	                setUserClient(result.data);
	            } else {
	                setLoadUserError(result.message);
	            }
	            setLoadUser(false);
	        }
	        
    const fetchAdmin = async () => {
	            setLoadUser(true);
	    
	            var result = await httpGet<Admin>(`/Admin/${params.id}`)
	            console.log(result)
	            if(result.isSuccess) {
	                setUserAdmin(result.data);
	            } else {
	                setLoadUserError(result.message);
	            }
	            setLoadUser(false);
	        }
	        
    
    const fetchCinemaBookingResource = async () => {
	            setLoadCinemaBookingResources(true);
	            setLoadErrorCinemaBookingResources(undefined);
	    
	            var result = await httpGet<Seat[]>("/Seat");
	    
	            if(result.isSuccess) {
	                setCinemaBookingResource(result.data)
	            } else {
	                setLoadErrorCinemaBookingResources(result.message);
	            }
	    
	            setLoadCinemaBookingResources(false);
	        }
	    
	        useEffect(() => {
	            fetchCinemaBookingResourceSchedules();
	        }, [selectedCinemaBookingResourceSchedule])
	    
	        const fetchCinemaBookingResourceSchedules = async () => {
	            setLoadCinemaBookingResourceSchedules(true)
	            setLoadErrorCinemaBookingResourceSchedules(undefined);
	    
	            var result = await httpGet<NightPlan[]>("/NightPlan");
	            if(result.isSuccess) {
	                setCinemaBookingResourceSchedules(result.data);
	            } else {
	                setLoadErrorCinemaBookingResourceSchedules(result.message);
	            }
	    
	            setLoadCinemaBookingResourceSchedules(false);
	        }
	        
	        const createCinemaBookingBooking = async () => {
	            setSubmittingCinemaBooking(true);
	            setSubmittingCinemaBookingError(undefined)
	    
	            var result = await httpPost<CreateCinemaBookingRequestModel>("/CinemaBooking", {
	            	seat: CinemaBookingResource.filter(e => e.id === selectedCinemaBookingResource)[0],
	            	plan: CinemaBookingResourceSchedules.filter(e => e.id === selectedCinemaBookingResourceSchedule)[0],
	                client: userClient
	          
	              
	            } as CreateCinemaBookingRequestModel)
	    
	            if(result.isSuccess) {
	                setSelectedCinemaBookingResource('');
	                setSelectedCinemaBookingResourceSchedule('')
	            } else {
	                setSubmittingCinemaBookingError(result.message)
	            }
	    
	            setSubmittingCinemaBooking(false)
	        }

    const render = () => {
	            return <div style={{display: "flex", width: "100%", justifyContent: "center", flexDirection: "column", padding: "20px"}}>
                    <Typography style={{textAlign: "center", width: "100%"}} variant="h2">Book resources</Typography>
                    <Typography style={{textAlign: "center", width: "100%"}} variant="h4">User: {params.id}, type: {params.type}</Typography>
                    {loadUser
                    ? <div style={{display: "flex", width: "100%", justifyContent: "center"}}><CircularProgress/></div>
                    : loadUserError
                    ? <Alert style={{margin: "10px 0"}} severity="error">
                        <AlertTitle>User load Error:</AlertTitle>
                        {loadUserError}
                    </Alert> 
                    : <div>
                    	<Accordion disabled={"Client" !== params.type} style={{width: "100%"}}>
		                    <AccordionSummary
		                    onClick={() => {
		                        if(!openCinemaBookingResource)fetchCinemaBookingResource();
		                        setOpenCinemaBookingResource(!openCinemaBookingResource);
		                    }}
		                    expandIcon={<ExpandMore/>}
		                    >
		                        <Typography>CinemaBooking</Typography>
		                    </AccordionSummary>
		                    <AccordionDetails style={{width: "100%"}}>
		                        <div style={{display: "flex", flexDirection: "column", width: "100%"}}>
		                            {loadCinemaBookingResources
		                            ? <div style={{display: "flex", width: "100%", justifyContent: "center"}}><CircularProgress/></div>
		                            : loadErrorCinemaBookingResources 
		                            ? <Alert style={{margin: "10px 0"}} severity="error">
		                                <AlertTitle>Error</AlertTitle>
		                                {loadErrorCinemaBookingResources}
		                            </Alert> 
		                            : <div style={{width: "100%"}}>
		                                {submittingCinemaBookingError 
		                                ?<Alert style={{margin: "10px 0"}} severity="error">
		                                    <AlertTitle>Error</AlertTitle>
		                                    {submittingCinemaBookingError}
		                                </Alert> : null}
		                                <FormControl style={{width: "100%"}} variant="outlined">
		                                    <InputLabel id="demo-simple-select-outlined-label">Seat</InputLabel>
		                                    <Select variant="outlined" value={selectedCinemaBookingResource} label={"Seat"} onChange={change => setSelectedCinemaBookingResource(change.target.value as string)}>
		                                    {CinemaBookingResource.map((ele, key) => {
		                                        return <MenuItem key={key} value={ele.id}>{ele.name}</MenuItem>
		                                    })}
		                                    </Select>
		                                </FormControl>
		                                <Collapse in={selectedCinemaBookingResource ? true : false}>
		                                    <div style={{padding: "20px 0"}}>
		                                        {loadCinemaBookingResourceSchedules
		                                        ? <div style={{display: "flex", width: "100%", justifyContent: "center"}}><CircularProgress/></div>
		                                        : <FormControl style={{width: "100%"}} variant="outlined">
		                                            <InputLabel id="demo-simple-select-outlined-label">NightPlan</InputLabel>
		                                            <Select variant="outlined" value={selectedCinemaBookingResourceSchedule} label={"NightPlan"} onChange={change => setSelectedCinemaBookingResourceSchedule(change.target.value as string)}>
		                                            {CinemaBookingResource.filter(e => e.id === selectedCinemaBookingResource)[0]?.nightPlans?.map((ele, key) => {
														return <MenuItem key={key} value={ele.id}>{ele.name}</MenuItem>
													})}
		                                            </Select>
		                                        </FormControl>
		                                        }
		                                        <Collapse in={selectedCinemaBookingResourceSchedule ? true : false}>
		                                            <div style={{paddingTop: "20px", width: "100%"}}>
		                                                {submittingCinemaBooking
		                                                ? <div style={{display: "flex", width: "100%", justifyContent: "center"}}><CircularProgress/></div>
		                                                : <Button style={{width: "100%"}} color="primary" variant="outlined" onClick={createCinemaBookingBooking}>Book CinemaBookingt</Button>}
		                                            </div>
		                                        </Collapse>
		                                    </div>
		                                </Collapse>
		                            <div style={{padding:"10px"}}/>
		                            </div>}
		                        </div>
		                    </AccordionDetails>
		                </Accordion>
                    </div>}
                </div>
	        }

    return render();
}

export default BookingPage;
