import React, { useState } from "react";
import { Button, Card, Checkbox, CircularProgress, Grid, TextField, MenuItem, Select, Typography, FormControl, InputLabel } from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import { httpGet, httpPost, httpPut } from "../../../api/httpClient";
import ChipInput from 'material-ui-chip-input'
import { useMount } from "../../../lifeCycleExtensions";
import ChipList from "../../../components/Chiplist";
import { useParams } from "react-router";
import { UpdateCinemaHallRequestModel } from "../../../api/requestModels/UpdateCinemaHallRequestModel"
import { CinemaHall } from "../../../api/models/entities/CinemaHall";
			import {Client} from "../../../api/models/customers/Client"

const UpdateCinemaHallPage = () => {

const params = useParams() as { id: string }

	const [submitting, setSubmitting] = useState(false);
	const [loading, setLoading] = useState(false);
	const [loadError, setLoadError] = useState<string>();
	const [error, setError] = useState<string>();
	const [success, setSuccess] = useState(false)

	const [name, setname] = useState<string>()
		
	const [city, setcity] = useState<string>()
		
	const [Imax, setImax] = useState<boolean>()
		
	const [cli, setcli] = useState<Client>()
		
	const [cliResult, setcliResult] = useState<Client[]>([])
	const [loadResult, setLoadResult] = useState<CinemaHall>();
	
	useMount(() => {
        load();
    })
    
    const load = async () => {
	            setLoading(true);
	    
	            const result = await httpGet<CinemaHall>(`/CinemaHall/${params.id}`)
	            if(result.isSuccess) {
	                setLoadResult(result.data)
	                setname(result.data.name)
	                setcity(result.data.city)
	                setImax(result.data.Imax)
	                setcli(result.data.cli)
	                downloadRelationData()
	            } else {
	                setLoadError(result.message)
	            }
	    
	            setLoading(false);
	        }
	        
	        
	            const downloadRelationData = async () => {
	            	setLoading(true);
	        		const cliResponse = await httpGet<Client[]>("/Client")
	        		if(cliResponse.isSuccess) {
	        			setcliResult(cliResponse.data)
	        		} else {
	        			setLoadError("Loading failed!")
	        		}
	        		
	        		setLoading(false);
	            }
	
	const submit = async () => {
        setSubmitting(true);
        setError(undefined);
        setSuccess(false);

        const result = await httpPut<UpdateCinemaHallRequestModel>("/CinemaHall", {
        	id: params.id,
            name: name, city: city, Imax: Imax, cli: cli
        } as UpdateCinemaHallRequestModel);

        if(result.isSuccess) {
        	setSuccess(true);
        } else {
			setError(result.statusCode +": "+ result.message);
        }

        setSubmitting(false);
    }
    
    const isNumber = (n: string | number): boolean => 
	            !isNaN(parseFloat(String(n))) && isFinite(Number(n));
	
	

    const renderBody = () => {
        if(loading) {
            return <div style={{width: "100%"}}><CircularProgress/></div>
        }
	
        return (
            <>
<TextField onChange={(e) => setname(e.target.value)} value={name} type="text" label="name" size="small" variant="outlined"></TextField>                 					
<div style={{padding:"10px"}}/>
	        			

<TextField onChange={(e) => setcity(e.target.value)} value={city} type="text" label="city" size="small" variant="outlined"></TextField>                 					
<div style={{padding:"10px"}}/>
	        			

<div style={{display: "flex", alignItems: "center"}}>
                <Checkbox onChange={e => setImax(e.target.checked)} value={Imax}/> Imax
            </div>
            <div style={{padding:"10px"}}/>
	        			

<FormControl variant="outlined">
<InputLabel id="demo-simple-select-outlined-label">cli</InputLabel>
<Select variant="outlined" label={"cli"} value={cli ? cli.id : undefined} onChange={(event) => setcli(cliResult.filter(e => e.id === event.target.value as string)[0])}>
	{cliResult.map((ele, key) => {
		return <MenuItem key={key} value={ele.id}>{ele.name}</MenuItem>
	})}
</Select>
</FormControl>
<div style={{padding:"10px"}}/>

	                    <div style={{padding:"10px"}}/>
	                    {submitting
	                    ? <div style={{width: "100%"}}><CircularProgress/></div>
	                    : <Button onClick={submit} variant="outlined" color="primary">Update</Button>}
            </>       
        )
    }
    
    const render = () => {
        return <div>
                    <Grid container style={{width: "100%", minHeight: "100vh"}} justify="center" alignItems="center">
                        <Grid item xs={10} sm={8} md={6} lg={4} xl={4}>
                            <Card style={{width: "100%", padding: "20px", display: "flex", justifyContent: "center", flexDirection: "column", textAlign: "center"}}> 
                                <Typography style={{paddingBottom: "10px"}} variant="h5">Update CinemaHall</Typography>
                                {success
	                            ? <Alert style={{margin: "10px 0"}} severity="success">
	                                <AlertTitle>Success</AlertTitle>
	                                CinemaHall was updated successfully
	                            </Alert>
	                            : null}
	                            {error || loadError
                                ? <Alert style={{margin: "10px 0"}} severity="error">
                                    <AlertTitle>Error</AlertTitle>
                                    {error ? error : loadError}
                                </Alert>
	                            : null}
	                            {loadError 
                                ? null
                                : renderBody()}
                            </Card>
                        </Grid>
                    </Grid>
                </div>
    }

    return render();
}

export default UpdateCinemaHallPage;
