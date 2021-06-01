import { Chip, makeStyles, Theme, Typography } from "@material-ui/core";
import { Cancel } from "@material-ui/icons";
import React from "react";

const useStyles = makeStyles((theme: Theme) => ({
    chipContainer: {
        "backgroundColor": "transparent",
        "display": "inline-block",
        "marginBottom": "20px"
    },
    chip: {
        "marginTop": "10px",
        "marginRight": "5px"
    }
}))

interface ChipListProps {
    selectedItems: string[]
    onRemoveItem: (item: string) => void;
    title?: string
}

const ChipList = (props: ChipListProps) => {

    const classes = useStyles();

    const {selectedItems, onRemoveItem, title} = props;

    const render = () => {
        return (
            <div className={classes.chipContainer}>
                {selectedItems.length > 0 ?
                    <div>
                        <Typography>{title}:</Typography>
                        {selectedItems.map((item, key) => {
                            return <Chip
                                key={key}
                                className={classes.chip}
                                label={item}
                                deleteIcon={<Cancel/>}
                                onDelete={() => onRemoveItem(item)}
                                onClick={() => onRemoveItem(item)}
                            />
                        })}
                    </div> : null
                }
            </div>
        )
    }

    return render();
}

export default ChipList;

