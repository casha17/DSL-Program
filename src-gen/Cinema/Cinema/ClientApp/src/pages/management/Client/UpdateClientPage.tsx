import React, { useState } from "react";
import { Button, Card, Checkbox, CircularProgress, Grid, TextField, MenuItem, Select, Typography, FormControl, InputLabel } from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import { httpGet, httpPost, httpPut } from "../../../api/httpClient";
import ChipInput from 'material-ui-chip-input'
import { useMount } from "../../../lifeCycleExtensions";
import ChipList from "../../../components/Chiplist";
import { useParams } from "react-router";
import { UpdateClientRequestModel } from "../../../api/requestModels/UpdateClientRequestModel"
import { Client } from "../../../api/models/customers/Client";

const UpdateClientPage = () => {

const params = useParams() as { id: string }

	const [submitting, setSubmitting] = useState(false);
	const [loading, setLoading] = useState(false);
	const [loadError, setLoadError] = useState<string>();
	const [error, setError] = useState<string>();
	const [success, setSuccess] = useState(false)

	const [name, setname] = useState<string>()
		
	const [age, setage] = useState<number>()
		
	const [height, setheight] = useState<number>()
		
	const [isVip, setisVip] = useState<boolean>()
		
	const [discount, setdiscount] = useState<number[]>([])
		
	const [loadResult, setLoadResult] = useState<Client>();
	
	useMount(() => {
        load();
    })
    
    const load = async () => {
	            setLoading(true);
	    
	            const result = await httpGet<Client>(`/Client/${params.id}`)
	            if(result.isSuccess) {
	                setLoadResult(result.data)
	                setname(result.data.name)
	                setage(result.data.age)
	                setheight(result.data.height)
	                setisVip(result.data.isVip)
	                setdiscount(result.data.discount)
	            } else {
	                setLoadError(result.message)
	            }
	    
	            setLoading(false);
	        }
	        
	
	const submit = async () => {
        setSubmitting(true);
        setError(undefined);
        setSuccess(false);

        const result = await httpPut<UpdateClientRequestModel>("/Client", {
        	id: params.id,
            name: name, age: age, height: height, isVip: isVip, discount: discount
        } as UpdateClientRequestModel);

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
	        			

<TextField onChange={(e) => setage(parseInt(e.target.value))} value={age} type="number" label="age" size="small" variant="outlined"></TextField>
<div style={{padding:"10px"}}/>
	        			

<TextField onChange={(e) => setheight(parseInt(e.target.value))} value={height} type="number" label="height" size="small" variant="outlined"></TextField>
<div style={{padding:"10px"}}/>
	        			

<div style={{display: "flex", alignItems: "center"}}>
                <Checkbox onChange={e => setisVip(e.target.checked)} value={isVip}/> isVip
            </div>
            <div style={{padding:"10px"}}/>
	        			

<ChipInput label={"discount"} variant="outlined" value={discount} onAdd={(chip) => {
                if(isNumber(chip)) {
                    setdiscount([...discount, parseInt(chip)])
                }
            }}
            onDelete={(chip, index) => {
                discount.splice(index, 1)
                setdiscount([...discount]);
            }}
            />
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
                                <Typography style={{paddingBottom: "10px"}} variant="h5">Update Client</Typography>
                                {success
	                            ? <Alert style={{margin: "10px 0"}} severity="success">
	                                <AlertTitle>Success</AlertTitle>
	                                Client was updated successfully
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

export default UpdateClientPage;
