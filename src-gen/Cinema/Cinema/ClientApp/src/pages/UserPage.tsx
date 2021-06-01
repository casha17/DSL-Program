import { Button, Card, Divider, Grid, Typography } from "@material-ui/core";
import React from "react";
import { useHistory, useParams } from "react-router";

const UserPage = () => {

    const params = useParams() as {id: string, type: string}
    const history = useHistory();

    const render = () => {
        return (
            <div>
                <Grid container style={{width: "100%", minHeight: "100vh"}} justify="center" alignItems="center">
                    <Grid item xs={10} sm={8} md={6} lg={4} xl={4}>
                        <Card style={{width: "100%", padding: "20px", display: "flex", justifyContent: "center", flexDirection: "column", textAlign: "center"}}> 
                            <Typography style={{paddingBottom: "10px"}} variant="h5">User: {params.id}</Typography>
                            <Typography style={{paddingBottom: "10px"}} variant="h5">Type: {params.type}</Typography>

                            <Divider style={{margin: "20px 0"}}></Divider>
                            
                            <div style={{padding: "20px 0", width: "100%"}}>
                                <Button style={{width: "100%"}} variant="outlined" color="primary" onClick={() => history.push(`/bookingoverview/${params.id}/${params.type}`)}>My bookings</Button>
                            </div>
                            <div style={{paddingBottom: "20px", width: "100%"}}>
                                <Button style={{width: "100%"}} variant="outlined" color="primary" onClick={() => history.push(`/booking/${params.id}/${params.type}`)}>Create Booking</Button>
                            </div>
                        </Card>
                    </Grid>
                </Grid>
            </div>
        )
    }

    return render();
}

export default UserPage;
