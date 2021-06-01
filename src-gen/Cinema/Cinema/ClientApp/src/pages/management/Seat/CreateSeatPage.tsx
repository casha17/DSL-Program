import React, { useState } from "react";
import { Button, Card, Checkbox, CircularProgress, Grid, TextField, MenuItem, Select, Typography, FormControl, InputLabel } from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import { httpGet, httpPost } from "../../../api/httpClient";
import ChipInput from 'material-ui-chip-input'
import { useMount } from "../../../lifeCycleExtensions";
import ChipList from "../../../components/Chiplist";
import { CreateSeatRequestModel } from "../../../api/requestModels/CreateSeatRequestModel";

import {NightPlan} from "../../../api/models/schedules/NightPlan"

const CreateSeatPage = () => {
	
	const [submitting, setSubmitting] = useState(false);
	const [loading, setLoading] = useState(false);
	const [loadError, setLoadError] = useState<string>();
	const [error, setError] = useState<string>();
	const [success, setSuccess] = useState(false)
	
	const [name, setname] = useState<string>()
	const [weight, setweight] = useState<number>()
	const [nightPlans, setnightPlans] = useState<NightPlan[]>([])
	const [nightPlansResult, setnightPlansResult] = useState<NightPlan[]>([])
	
			useMount(() => {
			        downloadRelationData();
			    })
	
	    const downloadRelationData = async () => {
	    	setLoading(true);
			const nightPlansResponse = await httpGet<NightPlan[]>("/NightPlan")
			if(nightPlansResponse.isSuccess) {
				setnightPlansResult(nightPlansResponse.data)
			} else {
				setLoadError("Loading failed!")
			}
			
			setLoading(false);
	    }

	const submit = async () => {
        setSubmitting(true);
        setError(undefined);
        setSuccess(false);

        const result = await httpPost<CreateSeatRequestModel>("/Seat", {
            name: name, weight: weight, nightPlans: nightPlans
        } as CreateSeatRequestModel);

        if(result.isSuccess) {
        	setname("")
        	    				
        	setweight(undefined)
        	    				
        	setnightPlans([])
        	
			setSuccess(true);
        } else {
			setError(result.statusCode +": "+ result.message);
        }

        setSubmitting(false);
    }
    
    const isNumber = (n: string | number): boolean => 
	            !isNaN(parseFloat(String(n))) && isFinite(Number(n));
	
	const updatenightPlans = (item: NightPlan, add: boolean) => {
		if(add) {
			nightPlans.push(item);
		} else {
			nightPlans.splice(nightPlans.indexOf(item), 1)
		} 
		setnightPlans([...nightPlans]);
	}
	
	
	const renderBody = () => {
        if(loading) {
            return <div style={{width: "100%"}}><CircularProgress/></div>
        }
	
        return (
            <>
                <TextField onChange={(e) => setname(e.target.value)} value={name} type="text" label="name" size="small" variant="outlined"></TextField>                 					
                <div style={{padding:"10px"}}/>
                	        			
	        			
	        			
                <TextField onChange={(e) => setweight(parseInt(e.target.value))} value={weight} type="number" label="weight" size="small" variant="outlined"></TextField>
                <div style={{padding:"10px"}}/>
                	        			
	        			
	        			
	        			
	        			
	                    <div style={{padding:"10px"}}/>
	                    {submitting
	                    ? <div style={{width: "100%"}}><CircularProgress/></div>
	                    : <Button onClick={submit} variant="outlined" color="primary">Create</Button>}
            </>       
        )
    }
    
    const render = () => {
        return <div>
                    <Grid container style={{width: "100%", minHeight: "100vh"}} justify="center" alignItems="center">
                        <Grid item xs={10} sm={8} md={6} lg={4} xl={4}>
                            <Card style={{width: "100%", padding: "20px", display: "flex", justifyContent: "center", flexDirection: "column", textAlign: "center"}}> 
                                <Typography style={{paddingBottom: "10px"}} variant="h5">Create Seat</Typography>
                                {success
	                            ? <Alert style={{margin: "10px 0"}} severity="success">
	                                <AlertTitle>Success</AlertTitle>
	                                Seat was created successfully
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

export default CreateSeatPage;
