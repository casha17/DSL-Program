import React, { useState } from "react";
import { Button, Card, Checkbox, CircularProgress, Grid, TextField, MenuItem, Select, Typography, FormControl, InputLabel } from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import { httpGet, httpPost, httpPut } from "../../../api/httpClient";
import ChipInput from 'material-ui-chip-input'
import { useMount } from "../../../lifeCycleExtensions";
import ChipList from "../../../components/Chiplist";
import { useParams } from "react-router";
import { UpdateSeatRequestModel } from "../../../api/requestModels/UpdateSeatRequestModel"
import { Seat } from "../../../api/models/resources/Seat";
			import {NightPlan} from "../../../api/models/schedules/NightPlan"

const UpdateSeatPage = () => {

const params = useParams() as { id: string }

	const [submitting, setSubmitting] = useState(false);
	const [loading, setLoading] = useState(false);
	const [loadError, setLoadError] = useState<string>();
	const [error, setError] = useState<string>();
	const [success, setSuccess] = useState(false)

	const [name, setname] = useState<string>()
		
	const [weight, setweight] = useState<number>()
		
	const [nightPlans, setnightPlans] = useState<NightPlan[]>([])
		
	const [nightPlansResult, setnightPlansResult] = useState<NightPlan[]>([])
	const [loadResult, setLoadResult] = useState<Seat>();
	
	useMount(() => {
        load();
    })
    
    const load = async () => {
	            setLoading(true);
	    
	            const result = await httpGet<Seat>(`/Seat/${params.id}`)
	            if(result.isSuccess) {
	                setLoadResult(result.data)
	                setname(result.data.name)
	                setweight(result.data.weight)
	                setnightPlans(result.data.nightPlans)
	                downloadRelationData()
	            } else {
	                setLoadError(result.message)
	            }
	    
	            setLoading(false);
	        }
	        
	        
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

        const result = await httpPut<UpdateSeatRequestModel>("/Seat", {
        	id: params.id,
            name: name, weight: weight, nightPlans: nightPlans
        } as UpdateSeatRequestModel);

        if(result.isSuccess) {
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
	        			

<ChipList selectedItems={nightPlans.map(e => e.id)} onRemoveItem={(item) => updatenightPlans(nightPlans.filter(e => e.id === item)[0], false)}></ChipList>
<FormControl variant="outlined">
<InputLabel id="demo-simple-select-outlined-label">nightPlans</InputLabel>
<Select variant="outlined" value={''} label={"nightPlans"} onChange={(value) => updatenightPlans(nightPlansResult.filter(e => e.id === value.target.value as string)[0], true)}>
	{nightPlansResult.filter(f => !nightPlans.map(e => e.id).includes(f.id)).map((ele, key) => {
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
                                <Typography style={{paddingBottom: "10px"}} variant="h5">Update Seat</Typography>
                                {success
	                            ? <Alert style={{margin: "10px 0"}} severity="success">
	                                <AlertTitle>Success</AlertTitle>
	                                Seat was updated successfully
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

export default UpdateSeatPage;
